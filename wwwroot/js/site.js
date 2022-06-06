// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function numberCheck(input, output) {
    if (new RegExp(/^[0-9]+$/).test(input)) {
        output.style.color = "green";
    } else {
        output.style.color = "red";
    }
}

function letterCheck(input, output) {
    if (new RegExp(/^[A-Za-züöäéúíóáèùìòàêûîôâ -]+$/).test(input)) {
        output.style.color = "green";
    } else {
        output.style.color = "red";
    }
}

function letter_countCheck(input, output) {
    if (input.length > 0) {
        output.style.color = "green";
    } else {
        output.style.color = "red";
    }
}

function KTO_countCheck(input, output) {
    if (input.length > 0 && input.length <= 10) {
        output.style.color = "green";
    } else {
        output.style.color = "red";
    }
}

function BLZ_countCheck(input, output) {
    if (input.length == 8) {
        output.style.color = "green";
    } else {
        output.style.color = "red";
    }
}

function PLZ_countCheck(input, output) {
    if (input.length == 5) {
        output.style.color = "green";
    } else {
        output.style.color = "red";
    }
}

function BIC_countCheck(input, output) {
    if (input.length == 8 || input.length == 11) {
        output.style.color = "green";
    } else {
        output.style.color = "red";
    }
}

function BIC_formatCheck(input, output) {
    if (new RegExp(/^[A-Za-z0-9]{4}[A-Za-z]{2}[A-Za-z0-9]{2}$/).test(input) || new RegExp(/^[A-Za-z0-9]{4}[A-Za-z]{2}[A-Za-z0-9]{2}[A-Za-z0-9]{3}$/).test(input)) {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "green";
        }
    } else {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "red";
        }
    }
}

function BIC_bankcodeCheck(input, output) {
    if (new RegExp(/^[A-Za-z0-9]{4}.*$/).test(input)) {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "green";
        }
    } else {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "red";
        }
    }
}

function BIC_laendercodeCheck(input, output) {
    if (new RegExp(/^.{4}[A-Za-z]{2}.*$/).test(input)) {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "green";
        }
    } else {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "red";
        }
    }
}

function BIC_ortscodeCheck(input, output) {
    if (new RegExp(/^.{4}.{2}[A-Za-z0-9]{2}.*$/).test(input)) {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "green";
        }
    } else {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "red";
        }
    }
}

function BIC_branchcodeCheck(input, output) {
    if (new RegExp(/^.{4}.{2}.{2}[A-Za-z0-9]{3}$/).test(input)) {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "green";
        };
        //output.forEach(span => {
        //    span.style.color = "green";
        //});
    } else if (new RegExp(/^.{4}.{2}.{2}[A-Za-z0-9]{1,2}$/).test(input) || new RegExp(/^.{4}.{2}.{2}[A-Za-z0-9]{4,}$/).test(input)) {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "red";
        };
        //output.forEach(span => {
        //    span.style.color = "red";
        //});
    } else {
        for (let index = 0; index < output.length; index++) {
            output[index].style.color = "black";
        };
        //output.forEach(span => {
        //    span.style.color = "black";
        //});
    }
}

function validation() {
    if (new RegExp(/^[0-9]+$/).test(document.getElementById("kontonummer").value) == false ||
        document.getElementById("kontonummer").value.length <= 0 || document.getElementById("kontonummer").value.length > 10 ||
        new RegExp(/^[0-9]+$/).test(document.getElementById('BLZ').value) == false ||
        document.getElementById("BLZ").value.length <= 0 || document.getElementById("BLZ").value.length > 8) {
        alert("ungültige Eingaben werden nicht gesendet")
    } else {
        document.getElementById("query_form").formmethod = "get"; // falsch
    }
}
