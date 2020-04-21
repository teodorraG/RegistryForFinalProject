HideMoreThan12();

function HideMoreThan12() {
    let items = document.getElementsByClassName('ShowMore');
    for (var i = 12; i < items.length; i++) {
        items[i].style.display = "none";
    }
    if (items.length <= 12) {
        HideViewMoreButton() 
    }
}

function ViewMore() {
    let items = document.getElementsByClassName('ShowMore');
    let multiplier = 0;
    for (var i = 12; i < items.length; i += 12) {
        if (items[i].style.display === "none") {
            console.log(i);
            multiplier = i / 12;
            break;
        }
    }
    for (var i = multiplier * 12; i < (multiplier + 1) * 12; i++) {
        if (items[i] === undefined) {
            break;
        }
        items[i].style.display = "block";
    }
    if (multiplier * 12 >= items.length - 12) {
        HideViewMoreButton() 
    }
}

function HideViewMoreButton() {
    let button = document.getElementById("ViewMoreButton");
    button.style.display = "none";
}