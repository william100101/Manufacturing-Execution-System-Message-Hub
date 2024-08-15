$(document).ready(function () {
    console.log("loading document ready");
    $('.nav-tabs a').click(function (e) {
        console.log("nav click");
        e.preventDefault();
        $(this).tab('show');
        if ($(this).is(".dropdown-menu li a")) {
            $("#dropdownTabName").html($(this).html());
            $("#dropDownTabsLink").dropdown("toggle");
        }
    });
});

function customAlert() {
    window.alert("test JS Interop");
}

function customPrettyPrint() {
    PR.prettyPrint();
}

function EnableTabs() {
    $('.nav-tabs').tab('show');
}

async function downloadFileFromStream(fileName, contentStreamReference) {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);

    triggerFileDownload(fileName, url);

    URL.revokeObjectURL(url);
}

function triggerFileDownload(fileName, url) {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}