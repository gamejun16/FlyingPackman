# OpenCV(C++)를 적용한 Unity 2D 게임 프로젝트

2020.03

# 로직

1. OpenCV 색깔 인식을 통해 화면상(웹캠 등의 카메라)에 가장 큰 범위로 인식된 빨간색의 x축 좌표를 인식

2. 인식한 결과를 동적 라이브러리(.dll)를 통해 유니티로 전달

3. 전달받은 값을 이용해 캐릭터가 이동되며 게임 진행

# Ingame
![PlayByCoke](https://user-images.githubusercontent.com/24224903/79637892-7c27b300-81bd-11ea-8550-b66422488dfb.gif)

빨간색만 있다면 코카콜라도 컨트롤러로 사용 가능.

![PlayByGear](https://user-images.githubusercontent.com/24224903/79637894-7df17680-81bd-11ea-9a8e-e7b656462416.gif)

노란 게이지가 가득 차면 잠시동안 레이저 발사 후 무기 강화

![boss](https://user-images.githubusercontent.com/24224903/79637889-79c55900-81bd-11ea-85c3-970b0763c27d.gif)

보스 시스템
