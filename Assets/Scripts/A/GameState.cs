// Assets/Scripts/Core/GameState.cs
public enum GameState
{
    Idle,               // 처음 상태 (불 꺼짐)
    GasOn_High,         // 불 강하게 켠 상태
    PotPlaced,          // 냄비 올림
    WaterAdded,         // 물 넣음
    IngredientsAdding,  // 소스, 떡, 어묵, 파, 양배추 넣는 단계
    Cooking_HighFlame,  // 재료 다 넣고 끓기 시작 (불이 강해서 방치하면 금방 탐!)
    Cooking_LowFlame,   // 불을 줄인 상태 (주걱으로 저어야 게이지가 오름)
    CookDone,           // 요리 완성! (접시에 담을 준비)
    Plating,            // 접시에 담음
    Complete,           // 서빙 완료 (성공)
    Failed              // 타버림 (실패)
}

public enum CookResult
{
    Perfect, Burnt
}
