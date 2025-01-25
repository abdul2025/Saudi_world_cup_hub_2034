// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Automatically add 'active' class based on the current URL
document.addEventListener('DOMContentLoaded', () => {
    const links = document.querySelectorAll('.nav-link');
    const currentPath = window.location.pathname;

    links.forEach(link => {
        const href = link.getAttribute('href') || link.dataset.url; // For ASP.NET routing
        if (href && currentPath.includes(href)) {
            link.classList.add('active');
        }
    });

    // Add click event to handle manual activation (if needed)
    links.forEach(link => {
        link.addEventListener('click', () => {
            links.forEach(l => l.classList.remove('active'));
            link.classList.add('active');
        });
    });
});



// JavaScript function to enable or disable the "Return Date" field
function toggleReturnDate(enable) {
    const returnDate = document.getElementById('returnDate');
    if (enable) {
        returnDate.removeAttribute('disabled');
    } else {
        returnDate.setAttribute('disabled', 'true');
        returnDate.value = ''; // Clear the value when disabled
    }
}