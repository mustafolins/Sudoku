// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function checkGuess(element, i, j) {
    // if correct change color to blue and pink if it is current selected number
    if (element.value == sudokuData[i][j]) {
        if (curSelectedNum == sudokuData[i][j]) {
            element.parentElement.style.backgroundColor = "pink"
        }
        else {
            element.parentElement.style.backgroundColor = "aqua"
        }
        shownValues[i][j] = true
    }
    // if incorrect value is there set to red
    else if (element.value != "") {
        element.parentElement.style.backgroundColor = "red"
        shownValues[i][j] = false
    }
    // if no value set to grey
    else {
        element.parentElement.style.backgroundColor = "grey"
        shownValues[i][j] = false
    }
    // if curr selected number then highlight it
    if (element.value == curSelectedNum) {
        element.parentElement.style.backgroundColor = "rgb(236, 125, 144)"
    }

    // is solved displya solved message
    if (isSolved()) {
        alert("It is solved");
    }
}

function isSolved() {
    var isSolved = true;
    $('td').each(function (index, element) {
        var row = $(element).parent().index()
        var col = $(element).index()
        var number = sudokuData[row][col]
        // has the correct element
        if (element.firstElementChild.value == number || element.firstElementChild.innerHTML == number) {
        }
        // doesn't have the correct element
        else {
            isSolved = false
        }
    })
    return isSolved;
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
        // is selected col or row
        if ($(element).parent().index() == row || $(element).index() == col) {
            //element.style.border = "dotted"
            //element.style.borderColor = "green"
            if (element.style.backgroundColor != "grey"
                && element.style.backgroundColor != "red"
                && element.style.backgroundColor != "rgb(236, 125, 144)") {
                element.style.backgroundColor = "lightpink"
            }
        }
        // not the selected col or row
        else {
            if (element.style.backgroundColor != "grey"
                && element.style.backgroundColor != "red"
                && element.style.backgroundColor != "rgb(236, 125, 144)") {
                element.style.backgroundColor = "white"
            }
        }
    })
}

function timerLabel() {
    if (isSolved()) {
        return
    }

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