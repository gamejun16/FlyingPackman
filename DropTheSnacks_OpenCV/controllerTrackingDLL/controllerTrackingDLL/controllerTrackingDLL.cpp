
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

		cv::ocl::setUseOpenCL(true); // OpenCL ��� �غ�

		initParameters();


		if (!cap.isOpened()) { // �� ķ�̶� ���� �����ִٸ� false ��ȯ �� ����

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

		/**************************		[ ��� ķ ] ���� ��ǥ(xPos, yPos) ����		***************************/
		Mat img_frame;
		UMat u_img_frame, u_img_hsv, u_img_cut;
		try {
			//// ������ �о����. ķ ȭ���� img_frame�� ����
			cap.read(img_frame);

			// ī�޶� �� ������ ����� ���
			if (img_frame.empty()) {
				return false;
			}

			u_img_frame = img_frame.getUMat(AccessFlag::ACCESS_READ);
			//u_img_cut = u_img_frame(Range(LEFT_TOP, LEFT_BOTTOM), Range(TOP_LEFT, TOP_RIGHT));
			u_img_cut = u_img_frame;
			GaussianBlur(u_img_cut, u_img_cut, Size(5, 5), 0);

			cvtColor(u_img_cut, u_img_hsv, COLOR_BGR2HSV); // color-based detection


			/********************** �� ��� Ʈ��ŷ ��ó�� **********************/

			//	//������ HSV ������ �̿��Ͽ� ������ ����ȭ
			UMat u_img_mask1;
			inRange(u_img_hsv, redLower, redUpper, u_img_mask1);

			

			// ��Ȯ���� ���̱� ���� ����. ���ɿ� ������ �ش�.
			//morphological opening ���� ������ ����
			erode(u_img_mask1, u_img_mask1, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
			dilate(u_img_mask1, u_img_mask1, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
			//morphological closing ������ ���� �޿��
			//dilate(u_img_mask1, u_img_mask1, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
			//erode(u_img_mask1, u_img_mask1, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

			//�󺧸�
			Mat img_mask1, img_labels, stats, centroids;
			img_mask1 = u_img_mask1.getMat(AccessFlag::ACCESS_READ);
			int numOfLables = connectedComponentsWithStats(img_mask1, img_labels, stats, centroids, 8, CV_32S);

			/********** �� ��� Ʈ��ŷ ��ó�� ���� ***********/
			/**************** �� �� ��� ������ ���� ���ÿ� ����� ��� ����  *****************/
				// 1. '��'���� �νĵ� ��ü�� �߽�(colorCenter)�� ����
				// 2. '���'���� �νĵ� ��ü���� ��ȸ�ϸ� '��'�ν��� ����� �߽� ��ǥ�� ��
				// 3. ��ġ�Ǵ� ��ü(����̸鼭 ���� ����� ��ü)�� ������ ���������� ������ ��� �� ��ȯ
				// 4. ���ٸ�?

				// ������ �νĵ� ��ü���� ��ȸ
			int max = -1, idx = 0;

			for (int j = 1; j < numOfLables; j++) {
				int area = stats.at<int>(j, CC_STAT_AREA);
				if (max < area)
				{
					// ���� ū �� ������ ��ü�� ����
					max = area;
					idx = j;
				}
			}

			// ��ü�� �ν� ������ ����Ƽ�� �Ѱܼ� �� ������ �̿��� ��ǥ(x, z��) Ž��
			int left = stats.at<int>(idx, CC_STAT_LEFT);
			int top = stats.at<int>(idx, CC_STAT_TOP);
			int width = stats.at <int>(idx, CC_STAT_WIDTH);
			int height = stats.at<int>(idx, CC_STAT_HEIGHT);

			*_center = left + (width / 2); // �νĵ� ��ü(��Ʈ�ѷ�)�� �߽� ��ǥ ����

			// �ν� ���� ǥ��
			rectangle(u_img_frame, Rect(Point(left, top), Point(left+width, top+height)), Scalar(0, 0, 255), 4);
		
			imshow("trackedColorMask", img_mask1);
			imshow("frontCam", u_img_frame);


			waitKey(1);

		}
		catch (Exception e) {
			return false;
		}
		/************ �̻� ���� ķ ************/

		return true;
	}


}