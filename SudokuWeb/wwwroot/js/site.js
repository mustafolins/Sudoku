// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function checkGuess(element, i, j) {
    if (element.value == sudokuData[i][j]) {
        if (curSelectedNum == sudokuData[i][j]) {
            element.parentElement.style.backgroundColor = "pink"
        }
        else {
            element.parentElement.style.backgroundColor = "aqua"
        }
        shownValues[i][j] = true
    }
    else if (element.value != "") {
        element.parentElement.style.backgroundColor = "red"
        shownValues[i][j] = false
    }
    else {
        element.parentElement.style.backgroundColor = "grey"
        shownValues[i][j] = false
    }
    if (element.value == curSelectedNum) {
        element.parentElement.style.backgroundColor = "rgb(236, 125, 144)"
    }
}

function highlightOption(number) {
    $('td').each(function (index, element) {
        // is selected number
        if (element.firstElementChild.value == number || element.firstElementChild.innerHTML == number) {
            //element.style.border = "dotted"
            //element.style.borderColor = "green"
            if (element.style.backgroundColor != "grey"
                && element.style.backgroundColor != "red") {
                element.style.backgroundColor = "rgb(236, 125, 144)"
            }
        }
        // not the selected number
        else {
            if (element.style.backgroundColor != "grey"
                && element.style.backgroundColor != "red"
                && element.style.backgroundColor != "lightpink") {
                element.style.backgroundColor = "white"
            }
        }
    })
    curSelectedNum = number
}

function highlightRowCol(row, col) {
    $('td').each(function (index, element) {
        // is selected number
        if ($(element).parent().index() == row || $(element).index() == col) {
            //element.style.border = "dotted"
            //element.style.borderColor = "green"
            if (element.style.backgroundColor != "grey"
                && element.style.backgroundColor != "red"
                && element.style.backgroundColor != "rgb(236, 125, 144)") {
                element.style.backgroundColor = "lightpink"
            }
        }
        // not the selected number
        else {
            if (element.style.backgroundColor != "grey"
                && element.style.backgroundColor != "red"
                && element.style.backgroundColor != "rgb(236, 125, 144)") {
                element.style.backgroundColor = "white"
            }
        }
    })
}

function getColorHex(r, g, b) {
    var red = rgbToHex(r);
    var green = rgbToHex(g);
    var blue = rgbToHex(b);
    return red + green + blue;
};

function timerLabel() {

    // Get today's date and time
    var now = new Date().getTime();

    // Find the distance between now and the count down date
    var distance = now - startDate;

    // Time calculations for days, hours, minutes and seconds
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    // Output the result in an element with id="demo"
    document.getElementById("timerElement").innerHTML = hours + "h " + minutes + "m " + seconds + "s ";
}