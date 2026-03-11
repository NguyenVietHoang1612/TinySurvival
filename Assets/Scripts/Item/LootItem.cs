using System;
using System.Collections;
using UnityEngine;

namespace GameRPG
{
    public class LootItem : MonoBehaviour
    {
        [Header("Setup")]
        private Rigidbody2D rb;
        private SpriteRenderer sp;
        private Animator animator;
        private Collider2D colider;

        [SerializeField] private Item_SO item_SO;
        [SerializeField] private int quantity;

        [SerializeField] private float pickUpRange = 0.1f;

        [SerializeField] private GameObject originPrefab;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            sp = GetComponentInChildren<SpriteRenderer>();
            colider = GetComponent<Collider2D>();

        }

        private void OnValidate()
        {
            if (Application.isPlaying) return;

            if (item_SO != null)
            {
                if (sp == null) sp = GetComponentInChildren<SpriteRenderer>();

                if (sp != null)
                {
                    UpdateAppearance();
                }
            }
        }

        private void OnEnable()
        {
            colider.enabled = true;
        }

        private void OnDisable()
        {
            colider.enabled = false;
        }

        private void UpdateAppearance()
        {
            name = item_SO.name;
            sp.sprite = item_SO.itemIcon;
        }

        public void Initialize(Item_SO item_SO, int quantity)
        {
            this.item_SO = item_SO;
            this.quantity = quantity;

            if (this.item_SO != null)
            {
                UpdateAppearance();
                canPickUp = false;
                sp.gameObject.SetActive(false);
                sp.gameObject.SetActive(true);
            }
            else
            {
                sp.gameObject.SetActive(false);
                Debug.LogWarning("Item is unname");
            }
        }

        public void Picked()
        {
            animator.Play("LootItem");
            colider.enabled = false;
            StartCoroutine(ReturnToPoolAfterDelay(0.3f));
        }

        public Item_SO GetItemData() => item_SO;
        public int GetQuantity() => quantity;

        private IEnumerator ReturnToPoolAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            item_SO = null;
            quantity = 0;
            colider.enabled = true;

            ObjectPoolManager.Instance.Return(originPrefab, gameObject);
        }

        public Item_SO Item
        {
            get { return item_SO; }
            private set { item_SO = value; }
        }

        public void SetItem(Item_SO item)
        {
            item_SO = item;
        }

        public int Quantity
        {
            get { return quantity; }
            private set { quantity = value; }
        }

        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public void SetOriginPrefab(GameObject prefab)
        {
            originPrefab = prefab;
        }
    }
}
