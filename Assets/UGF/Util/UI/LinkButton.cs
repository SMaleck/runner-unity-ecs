using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UGF.Util.UI
{
    public class LinkButton : Button
    {
        [SerializeField] public string Url;
        
        protected override void Awake()
        {
            base.Awake();

            #if !UNITY_WEBGL

            this.OnClickAsObservable()
                .Subscribe(_ => { Application.OpenURL(Url); })
                .AddTo(this);
            
            #endif
        }
    }
}
