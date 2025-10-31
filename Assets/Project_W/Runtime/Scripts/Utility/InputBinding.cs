namespace S
{
    using System.Collections.Generic;
    using UnityEngine;
    using AYellowpaper.SerializedCollections;
    public enum UserAction
    {
        MoveForward,
        MoveBackward,
        MoveLeft,
        MoveRight,

        Run,
        Walk,

        Interact,
        Cancel,

        RideBroomStick,

        ToolSelector,
        PotionSelector,
        MemoBoard,
        WishList,

        UI_Inventory,
        UI_Encyclopedia,

    }
    public class InputBinding : MonoBehaviour
    {
        //private Dictionary<UserAction, KeyCode> _bindingDict;
        public SerializedDictionary<UserAction, KeyCode> _bindingDict;
        public void Awake()
        {
            ResetAll();
        }
        public void ApplyNewBindings(InputBinding newBinding)
        {
            _bindingDict = new SerializedDictionary<UserAction, KeyCode>(newBinding._bindingDict);
        }
        public void Bind(in UserAction action, in KeyCode code, bool allowOverlap = false)
        {
            if (!allowOverlap && _bindingDict.ContainsValue(code))
            {
                var copy = new Dictionary<UserAction, KeyCode>(_bindingDict);
                foreach (var pair in copy)
                {
                    if (pair.Value.Equals(code))
                    {
                        _bindingDict[pair.Key] = KeyCode.None;
                    }
                }
            }
            _bindingDict[action] = code;
        }
        public void ResetAll()
        {
            Bind(UserAction.MoveForward, KeyCode.UpArrow);
            Bind(UserAction.MoveBackward, KeyCode.DownArrow);
            Bind(UserAction.MoveLeft, KeyCode.LeftArrow);
            Bind(UserAction.MoveRight, KeyCode.RightArrow);

            Bind(UserAction.Run, KeyCode.Space);
            Bind(UserAction.Walk, KeyCode.LeftShift);

            Bind(UserAction.Interact, KeyCode.Z);
            Bind(UserAction.Cancel, KeyCode.X);

            Bind(UserAction.RideBroomStick, KeyCode.LeftControl); //빗자루 타기

            Bind(UserAction.ToolSelector, KeyCode.C); //도구 선택기
            Bind(UserAction.PotionSelector, KeyCode.A); //물약 선택기
            //물약 선택 후 조준
            //z누르는 상태에서 화살표로 위치 조절, z up 발사

            Bind(UserAction.MemoBoard, KeyCode.Tab); //메모 보드
            Bind(UserAction.WishList, KeyCode.Q); //찜 목록
            //화살표로 이동
            //1 채집물
            //2 합계 필요 재료
            //3 등록 목록

            Bind(UserAction.UI_Encyclopedia, KeyCode.D); //백과사전
            //q(<-) e(->)
            //left 지도
            //up 도감/조제법
            //right 모험일지

            Bind(UserAction.UI_Inventory, KeyCode.S); //인벤
        }
    }

}
