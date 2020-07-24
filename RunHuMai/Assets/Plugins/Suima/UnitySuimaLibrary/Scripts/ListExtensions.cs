using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Suima {
    public static class ListExtensions {
        /// <summary>
        /// 右にスライド(ループする)
        /// </summary>
        public static IList<T> RightSlide<T>(this IList<T> self) {
            var temp = self[self.Count - 1];
            foreach (var i in Enumerable.Range(0,self.Count - 1).Reverse()) {
                self[i + 1] = self[i];
            }
            self[0] = temp;
            return self;
        }
        /// <summary>
        /// 左にスライド(ループする)
        /// </summary>
        public static IList<T> LeftSlide<T>(this IList<T> self) {
            var temp = self[0];
            foreach (var i in Enumerable.Range(1,self.Count - 1)) {
                self[i - 1] = self[i];
            }
            self[self.Count - 1] = temp;
            return self;
        }
        /// <summary>
        /// vecが正の値なら右に、負の値なら左にスライドする
        /// </summary>
        public static IList<T> Slide<T>(this IList<T> self,int vec)
            => vec > -1 ? self.RightSlide() : self.LeftSlide();
        
        /// <summary>
        /// 指定したインデックスのアイテムを返し削除する
        /// </summary>
        public static T IndexPop<T>(this IList<T> self, int index) {
            var temp = self[index];
            self.RemoveAt(index);
            return temp;
        }
        /// <summary>
        /// 先頭にアイテムを追加する
        /// </summary>
        public static void Push<T>(this IList<T> self,T item) {
            self.Insert(0,item);
        }
        /// <summary>
        /// 先頭のアイテムを削除しつつ返す
        /// </summary>
        public static T Pop<T>(this IList<T> self) 
            => IndexPop(self, 0);
        

        /// <summary>
        /// ランダムなアイテムを返してリストから消す
        /// </summary>
        public static T RandomPop<T>(this IList<T> self) {
            Random random = new Random();
            var idx = random.Next(0, self.Count);
            var temp = self[idx];
            self.RemoveAt(idx);
            return temp;
        }
        /// <summary>
        /// リストをシャッフルする
        /// </summary>
        public static IList<T> Shuffle<T>(this IList<T> self) {
            Random random = new Random();
            foreach (var i in Enumerable.Range(0, self.Count)) {
                var idx = random.Next(0, self.Count);
                var temp = self[i];
                self[i] = self[idx];
                self[idx] = temp;
            }
            return self;
        }
    }
    
}
