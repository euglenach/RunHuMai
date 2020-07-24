using UnityEngine;
using UnityEngine.UI;

namespace Suima.Extensions{
    public static class UIExtensions{
        public static void SetAlpha(this Graphic target,float alpha){
            var col = target.color;
            target.color = new Color(col.r,col.g,col.b,alpha);
        }

        public static void SetAlpha(this SpriteRenderer target, float alpha){
            var col = target.color;
            target.color = new Color(col.r,col.g,col.b,alpha);
        }
    }
}
