// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const menuButton = document.getElementById("menuButton");
    const mainNav = document.getElementById("mainNav");
    const menuIcon = document.getElementById("menu");
    const menuOpenText = menuButton.getAttribute("data-menu-text");
    const menuCloseText = menuButton.getAttribute("data-close-text");

    menuButton.addEventListener("click", function () {
        if (mainNav.classList.contains("close") || mainNav.classList.contains("close-anim")) {
            mainNav.classList.remove("close", "close-anim");
            mainNav.classList.add("open-anim");
            menuText.classList.add("fade-out");
            setTimeout(() => {
                menuText.textContent = menuCloseText;
                menuText.classList.remove("fade-out");
                menuText.classList.add("fade-in");
            }, 300);
            menuIcon.id = "menu-open";
            menuIcon.classList.add("rotate");
        } else {
            mainNav.classList.remove("open-anim");
            mainNav.classList.add("close-anim");
            menuText.classList.add("fade-out");
            setTimeout(() => {
                menuText.textContent = menuOpenText;
                menuText.classList.remove("fade-out");
                menuText.classList.add("fade-in");
            }, 300);
            menuIcon.id = "menu";
            menuIcon.classList.add("rotate");
        }

        setTimeout(() => {
            menuIcon.classList.remove("rotate");
        }, 900);

        toggleMainContentMargin();
    });

    function toggleMainContentMargin() {
        const mainContent = document.querySelector('.main-content');
        const marginOffset = 1;

        if (mainNav.classList.contains("open-anim")) {
            mainContent.style.marginTop = `${mainNav.offsetHeight - marginOffset}px`;
        } else {
            mainContent.style.marginTop = '0';
        }
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const cards = document.querySelectorAll('.card-1, .card-2, .card-3, .card-4');
    const changePoint = 100;

    window.addEventListener("scroll", function () {
        const scrollTop = window.pageYOffset || document.documentElement.scrollTop;

        cards.forEach(card => {
            if (scrollTop > changePoint) {
                card.classList.remove('partially-visible');
                card.classList.add('fully-visible');
            } else {
                card.classList.remove('fully-visible');
                card.classList.add('partially-visible');
            }
        });
    });
});

function loadContent(url) {
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $('#mainContent').html(data);
            window.history.pushState({ path: url }, '', url);
        },
        error: function () {
            alert('Error loading content!');
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    var programBlocks = document.querySelectorAll('.acc-program-block');

    programBlocks.forEach(function (block) {
        var title = block.querySelector('.acc-program-title');
        var documents = block.querySelector('.acc-program-documents');

        documents.style.maxHeight = '0';
        documents.style.overflow = 'hidden';
        documents.style.transition = 'max-height 0.3s ease-out, margin-top 0.3s ease-out';

        title.addEventListener('click', function () {
            if (!documents.classList.contains('open')) {
                documents.style.maxHeight = documents.scrollHeight + 'px';
                documents.classList.add('open');
                title.classList.add('open');
            } else {
                documents.style.maxHeight = '0';
                documents.classList.remove('open');
                title.classList.remove('open');
            }
        });
    });

    var pathSegments = window.location.pathname.split('/');
    var programId = pathSegments[pathSegments.length - 1];
    if (programId) {
        var programBlock = document.querySelector(`#acc-program-block-${programId}`);
        if (programBlock) {
            var title = programBlock.querySelector('.acc-program-title');
            var documents = programBlock.querySelector('.acc-program-documents');

            documents.style.maxHeight = documents.scrollHeight + 'px';
            documents.classList.add('open');
            title.classList.add('open');
        }
    }
});

document.addEventListener('DOMContentLoaded', function () {
    var programBlocks = document.querySelectorAll('.program-card');

    programBlocks.forEach(function (block) {
        var title = block.querySelector('.program-card-title');
        var description = block.querySelector('.program-card-description');

        description.style.maxHeight = '0';
        description.style.overflow = 'hidden';
        description.style.transition = 'max-height 0.3s ease-out, margin-top 0.3s ease-out';

        title.addEventListener('click', function () {
            if (!description.classList.contains('open')) {
                description.style.maxHeight = description.scrollHeight + 'px';
                description.classList.add('open');
                title.classList.add('open');
            } else {
                description.style.maxHeight = '0';
                description.classList.remove('open');
                title.classList.remove('open');
            }
        });
    });
});

document.querySelector('.menu-icon').addEventListener('click', function () {
    document.querySelector('.language-switcher-form select').focus();
});