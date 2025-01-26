// Set the date we're counting down to (example: FIFA World Cup 2034 start date)
var countdownDate = new Date("Nov 21, 2034 00:00:00").getTime();

// Update the countdown every 1 second
var x = setInterval(function() {
    var now = new Date().getTime();
    var distance = countdownDate - now;

    // Time calculations for days, hours, minutes, and seconds
    var days = Math.floor(distance / (1000 * 60 * 60 * 24));
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    // Display the result
    document.getElementById("countdown").innerHTML = 
    "<span style='font-size: 1rem;'>" + days + " Days</span> " +
    "<span style='font-size: 1rem;'>" + hours + " Hours</span> " +
    "<span style='font-size: 1rem;'>" + minutes + " Minutes</span> " +
    "<span style='font-size: 1rem;'>" + seconds + " Seconds</span>";

    // If the countdown is over, display a message
    if (distance < 0) {
        clearInterval(x);
        document.getElementById("countdown").innerHTML = "The World Cup has Started!";
    }
}, 1000);
