
  const selectBox = document.querySelectorAll('.ali-select-box');
  const selectOption = document.querySelectorAll('.ali-select-option');
  const soValue = document.querySelectorAll('.ali-soValue');
  const optionSearch = document.querySelectorAll('.ali-optionSearch');
  const options = document.querySelectorAll('.ali-options');
  //const optionsList = selectBox.querySelectorAll('.ali-options li');

  selectOption.forEach((item, index) => {
    item.addEventListener('click', function () {
      selectBox[index].classList.toggle('ali-active');
    });
  });
selectBox.forEach((item, index) => {
  var optionsList = selectBox[index].querySelectorAll('.ali-options li');
  optionsList.forEach(function (optionsListSingle) {
    optionsListSingle.addEventListener('click', function () {
      text = this.textContent;
      soValue[index].value = text;
      item.classList.remove('ali-active');
    })
  });
});
optionSearch.forEach((item, index) => {
  item.addEventListener('keyup', function () {
    var filter, li, i, textValue;
    filter = item.value.toUpperCase();
    li = options[index].getElementsByTagName('li');
    for (i = 0; i < li.length; i++) {
      liCount = li[i];
      textValue = liCount.textContent || liCount.innerText;
      if (textValue.toUpperCase().indexOf(filter) > -1) {
        li[i].style.display = '';
      } else {
        li[i].style.display = 'none';
      }
    }
  });
});