$(document).ready(function () {
    var max_fields = 20; //maximum input boxes allowed
    var wrapper = $(".input_fields_wrap"); //Fields wrapper
    var add_button = $(".add_field_button"); //Add button ID

    function getPrevInput(elem) {
        var i = 0,
            inputs = document.getElementsByTagName('input'),
            ret = 'Not found';
        while (inputs[i] !== elem || i >= inputs.length) {
            if (inputs[i].type === 'text') {
                ret = inputs[i];
            }
            i++;
        }
        return ret.id;
    }

    var temp = "0";
    if (getPrevInput(positioner) !== 'DiscountPrice' && getPrevInput(positioner) !== 'disabledInput') {
        temp = getPrevInput(positioner).replace("Value", "");
    }
    var x = parseInt(temp);

    $(add_button).click(function (e) { //on add input button click
        e.preventDefault();
        if (x < max_fields) { //max input box allowed
            x++; //text box increment
            $(wrapper).append('<div class="lastToRemove' + x + ' mb-2"><input class="col-5 form-control d-inline-block" type="text" id ="Key' + x + '"' +  'x name="Key' +
                x + '"/> - <input class="col-5 form-control d-inline-block" type="text" id="Value' + x + '"' + 'name="Value' +
                x + '"/><a href="#" class="remove_field btn btn-danger mb-1 ml-2"><i class="far fa-trash-alt"></i></a></div>');
            //add input box
        }
    });
    $(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
        var lastToRemove = getPrevInput(positioner).replace("Value", "");
        e.preventDefault(); $('.lastToRemove' + lastToRemove).remove(); x--;
    })
});
