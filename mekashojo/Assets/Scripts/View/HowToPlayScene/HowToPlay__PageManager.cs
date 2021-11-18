using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HowToPlay__PageManager : MonoBehaviour
    {
        private const int INITIAL_PAGE_NUMBER = 0;

        [SerializeField, Header("ページを全て入れる")] private HowToPlay__Page[] _pages;
        [SerializeField, Header("PageNumberを入れる")] private Text _pageNumber;
        [SerializeField, Header("ForwardButtonを入れる")] private HowToPlay__ForwardButton _forwardButton;
        [SerializeField, Header("BackButtonを入れる")] private HowToPlay__BackButton _backButton;

        private int _currentPage = 0;

        public int currentPage
        {
            get { return _currentPage; }
            set
            {
                UpdatePage(value, _currentPage);
                _currentPage = value;
            }
        }

        private void Start()
        {
            for (int i = 0; i < _pages.Length; i++)
            {
                _pages[i].isActive = false;
            }

            _pages[INITIAL_PAGE_NUMBER].isActive = true;

            _pageNumber.text = (INITIAL_PAGE_NUMBER + 1).ToString();

            UpdateChangePageButton(INITIAL_PAGE_NUMBER);
        }

        private void UpdatePage(int newPageNumber, int previousPageNumber)
        {
            _pages[previousPageNumber].isActive = false;
            _pages[newPageNumber].isActive = true;
            _pageNumber.text = (newPageNumber + 1).ToString();

            UpdateChangePageButton(newPageNumber);
        }

        private void UpdateChangePageButton(int currentPageNumber)
        {
            // ForwardButtonをactiveにするかどうかの処理
            if (currentPageNumber == _pages.Length - 1 && _forwardButton.isActive)
            {
                _forwardButton.isActive = false;
            }
            else if (currentPageNumber != _pages.Length - 1 && !_forwardButton.isActive)
            {
                _forwardButton.isActive = true;
            }

            // BackButtonをactiveにするかどうかの処理
            if (currentPageNumber == 0 && _backButton.isActive)
            {
                _backButton.isActive = false;
            }
            else if (currentPageNumber != 0 && !_backButton.isActive)
            {
                _backButton.isActive = true;
            }
        }
    }
}
