# OpenCV(C++)를 적용한 Unity 2D 게임 프로젝트

2020.03

+ 캠 화면이 좌우가 반전되어서 뒤집어져 보일 뿐, 실제는 좌우 움직이는 방향대로 캐릭터가 움직임.

# 로직
0. 빨간색 물체를 컨트롤러로 사용

1. OpenCV 색깔 인식을 통해 화면상(웹캠 등의 카메라)에 가장 큰 범위로 인식된 빨간색의 x축 좌표를 인식

2. 인식한 결과를 동적 라이브러리(.dll)를 통해 유니티로 전달

3. 전달받은 값을 이용해 캐릭터가 이동되며 게임 진행

# YoutubeLink

[![VIDEO](https://img.youtube.com/vi/41QFLQyg_2E/0.jpg)](https://www.youtube.com/watch?v=41QFLQyg_2E?t=0s)
