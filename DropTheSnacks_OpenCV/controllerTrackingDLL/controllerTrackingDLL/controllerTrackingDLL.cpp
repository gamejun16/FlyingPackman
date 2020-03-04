
#include "pch.h"
#include "opencv2/opencv.hpp"
#include "opencv2/core.hpp"
#include "opencv2/imgproc.hpp"
#include "opencv2\core\ocl.hpp"


using namespace cv;
using namespace std;

extern "C" {

	VideoCapture cap;
	
	
	void initParameters() {

	}

	__declspec(dllexport) bool cv_TrackingOn() {

		cv::ocl::setUseOpenCL(true); // OpenCL 사용 준비

		initParameters();


		if (!cap.isOpened()) { // 한 캠이라도 아직 켜져있다면 false 반환 및 종료

			//cap.set(cv::CAP_PROP_SETTINGS, 0);
			//cap2.set(cv::CAP_PROP_SETTINGS, 0);

			cap.open(0);

			//cap.set(CAP_PROP_AUTOFOCUS, 0);

			//cap.set(CAP_PROP_AUTO_EXPOSURE, 0.25);


			cap.set(CAP_PROP_EXPOSURE, -4);

			return true;
		}
		return false;
	}

	__declspec(dllexport) bool cv_TrackingOff() {

		destroyAllWindows();

		cap.release();

		return true;
	}



	__declspec(dllexport) bool cv_Tracking(int* _center)
	{

		Scalar red(0, 0, 255);
		Scalar blue(255, 0, 0);
		Scalar yellow(0, 255, 255);
		Scalar magenta(255, 0, 255);
		Scalar lime(0, 238, 179); // lime color
		//Scalar greenLower(29, 86, 6), greenUpper(64, 255, 255);
		Scalar greenLower(29, 86, 6), greenUpper(64, 255, 255);

		Scalar redLower(160, 100, 100), redUpper(179, 255, 255);

		/**************************		[ 상단 캠 ] 공의 좌표(xPos, yPos) 검출		***************************/
		Mat img_frame;
		UMat u_img_frame, u_img_hsv, u_img_cut;
		try {
			//// 프레임 읽어오기. 캠 화면을 img_frame에 저장
			cap.read(img_frame);

			// 카메라에 빈 영상이 담겼을 경우
			if (img_frame.empty()) {
				return false;
			}

			u_img_frame = img_frame.getUMat(AccessFlag::ACCESS_READ);
			//u_img_cut = u_img_frame(Range(LEFT_TOP, LEFT_BOTTOM), Range(TOP_LEFT, TOP_RIGHT));
			u_img_cut = u_img_frame;
			GaussianBlur(u_img_cut, u_img_cut, Size(5, 5), 0);

			cvtColor(u_img_cut, u_img_hsv, COLOR_BGR2HSV); // color-based detection


			/********************** 색 기반 트래킹 전처리 **********************/

			//	//지정한 HSV 범위를 이용하여 영상을 이진화
			UMat u_img_mask1;
			inRange(u_img_hsv, redLower, redUpper, u_img_mask1);

			

			// 정확도를 높이기 위한 구문. 성능에 영향을 준다.
			//morphological opening 작은 점들을 제거
			erode(u_img_mask1, u_img_mask1, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
			dilate(u_img_mask1, u_img_mask1, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
			//morphological closing 영역의 구멍 메우기
			//dilate(u_img_mask1, u_img_mask1, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
			//erode(u_img_mask1, u_img_mask1, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

			//라벨링
			Mat img_mask1, img_labels, stats, centroids;
			img_mask1 = u_img_mask1.getMat(AccessFlag::ACCESS_READ);
			int numOfLables = connectedComponentsWithStats(img_mask1, img_labels, stats, centroids, 8, CV_32S);

			/********** 색 기반 트래킹 전처리 종료 ***********/
			/**************** 색 및 모양 추적에 대해 동시에 검출된 대상 지정  *****************/
				// 1. '색'으로 인식된 물체의 중심(colorCenter)을 저장
				// 2. '모양'으로 인식된 물체들을 순회하며 '색'인식의 결과와 중심 좌표를 비교
				// 3. 일치되는 물체(녹색이면서 공의 모양인 물체)를 공으로 결정내리며 정보를 출력 및 반환
				// 4. 없다면?

				// 색으로 인식된 물체들을 순회
			int max = -1, idx = 0;

			for (int j = 1; j < numOfLables; j++) {
				int area = stats.at<int>(j, CC_STAT_AREA);
				if (max < area)
				{
					// 가장 큰 색 범위의 물체를 저장
					max = area;
					idx = j;
				}
			}

			// 물체의 인식 정보를 유니티로 넘겨서 그 정보를 이용해 좌표(x, z축) 탐색
			int left = stats.at<int>(idx, CC_STAT_LEFT);
			int top = stats.at<int>(idx, CC_STAT_TOP);
			int width = stats.at <int>(idx, CC_STAT_WIDTH);
			int height = stats.at<int>(idx, CC_STAT_HEIGHT);

			*_center = left + (width / 2); // 인식된 물체(컨트롤러)의 중심 좌표 전달

			// 인식 범위 표시
			rectangle(u_img_frame, Rect(Point(left, top), Point(left+width, top+height)), Scalar(0, 0, 255), 4);
		
			imshow("trackedColorMask", img_mask1);
			imshow("frontCam", u_img_frame);


			waitKey(1);

		}
		catch (Exception e) {
			return false;
		}
		/************ 이상 전면 캠 ************/

		return true;
	}


}