function check_quantity(value) {
    var c_value = 0;
    var sm1 = document.getElementById('quantity:' + value);
    for (var i = 0; i < sm1.length; i++) {
        if (sm1[i].selected) { c_value = (eval(c_value) + eval(sm1[i].value)); }
    }
    var priceValue = document.getElementById('constPrice:' + value).innerHTML;
    var floatSymbolParse = priceValue.replace(',', '.');
    var sum = c_value * floatSymbolParse;
    document.getElementById('price:' + value).innerHTML = sum.toFixed(2);
}

function calculate_total() {

    var arr = document.getElementsByTagName('calculatedPrice')
    var sum = 0;

    for (var i = 0; i < arr.length; i++) {
        var floatSymbolParse = arr[i].innerHTML.replace(',', '.')
        sum += parseFloat(floatSymbolParse)
    }

    document.getElementById('result').innerHTML = sum.toFixed(2);
}
calculate_total();
