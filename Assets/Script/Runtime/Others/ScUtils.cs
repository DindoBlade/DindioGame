using UnityEngine;

namespace Dindio.Runtime.Others {
    public static class ScUtils {
        /// <summary>
        /// Check if the other transform is the same as the self or a child of the self
        /// </summary>
        /// <param name="other">The other Transform</param>
        /// <param name="self"> The self Transform</param>
        /// <returns>True if the other transform is the same as the self or a child of the self</returns>
        public static bool IsMyself(Transform other, Transform self) {
            return other == self || other.IsChildOf(self.transform);
        }
    }
}