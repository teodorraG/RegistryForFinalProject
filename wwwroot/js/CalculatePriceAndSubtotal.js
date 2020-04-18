
function CalculateNewPrice(i) {
    var price = document.getElementById("Price" + i).innerText.substring(1, document.getElementById("Price" + i).innerText.length);
    var subtotal = document.getElementById("Subtotal" + i);
    var quantity = document.getElementById("Quantity" + i);
    subtotal.innerText = "$" + (price * quantity.value).toFixed(2);
    CalculateTotal();
    
}

function CalculateTotal() {
    var total = 0;
    for (var j = 0; j < Infinity; j++) {
        var totalQuantity = document.getElementById("Quantity" + j);
        if (totalQuantity === null) {
            break;
        }
        var price = document.getElementById("Price" + j).innerText.substring(1, document.getElementById("Price" + j).innerText.length);
        total += (price * totalQuantity.value);
    }
    var totalLabel = document.getElementById("Total");
    totalLabel.innerText = total.toFixed(2);
}

var total1 = 0;
for (var j = 0; j < Infinity; j++) {
    var totalQuantity1 = document.getElementById("Quantity" + j);
    if (totalQuantity1 === null) {
        break;
    }
    var price1 = document.getElementById("Price" + j).innerText.substring(1, document.getElementById("Price" + j).innerText.length);
    total1 += (price1 * totalQuantity1.value);
}
var totalLabel1 = document.getElementById("Total");
totalLabel1.innerText = total1.toFixed(2);