document.getElementById("go-to-time-input").addEventListener("click", () => {
    let month = document.getElementById("month-input").value;
    let year = document.getElementById("year-input").value;

    window.location.search = `?y=${year}&m=${month}`;
});