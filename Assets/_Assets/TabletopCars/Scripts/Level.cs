using System.Linq;
using UnityEngine;

namespace TabletopCars.Scripts
{
    public class Level : MonoBehaviour
    {
        public string LevelTitle;
        public Transform CarStartingPosition;

        public int GetCollectibleAmount()
        {
            return (from Transform obj in transform select obj.GetComponent<Collectible>() into collectible where collectible != null select collectible.ScoreAmount).Sum();
        }
    }
}