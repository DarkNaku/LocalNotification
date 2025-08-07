# LocalNotification

### 소개
유니티의 모바일 노티피케이션 사용을 도와주는 헬퍼 패키지 입니다.

### 설치방법
1. 패키지 관리자의 툴바에서 좌측 상단에 플러스 메뉴를 클릭합니다.
2. 추가 메뉴에서 Add package from git URL을 선택하면 텍스트 상자와 Add 버튼이 나타납니다.
3. https://github.com/DarkNaku/LocalNotification.git?path=/Assets/LocalNotification 입력하고 Add를 클릭합니다.

### 사용법

1. 빈 GameObject를 만듭니다.
2. LocalNotification 컴포넌트를 추가합니다.
3. MessageDataGroup에 메세지 그룹들을 추가합니다. 메시지가 그룹 안에서 랜덤으로 선택됩니다.
4. Schedules에 노출할 시간들을 등록합니다. (AfterDays : 이후날짜 만약 1이면 하루뒤, Hours : 지정시간, Minutes : 지정분, Seconds : 지정초)
