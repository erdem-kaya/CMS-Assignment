// Mobile navigation menu toggle functionality
(function () {
    const header = document.querySelector('.navbar-header');
    const toggle = document.querySelector('.navbar-toggle');
    const panel = document.getElementById('primary-nav');

    if (!header || !toggle || !panel) return;
    panel.classList.add('nav-panel');

    const closeMenu = () => {
        header.classList.remove('is-open');
        toggle.setAttribute('aria-expanded', 'false');
    };

    const openMenu = () => {
        header.classList.add('is-open');
        toggle.setAttribute('aria-expanded', 'true');
    };

    const toggleMenu = () => {
        const isOpen = header.classList.contains('is-open');
        isOpen ? closeMenu() : openMenu();
    };

    toggle.addEventListener('click', toggleMenu);

    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') closeMenu();
    });

    panel.addEventListener('click', function (e) {
        const a = e.target.closest('a');
        if (a) closeMenu();
    });

    document.addEventListener('click', function (e) {
        if (!header.contains(e.target)) closeMenu();
    });

    window.addEventListener('resize', function () {
        if (window.innerWidth >= 1024) closeMenu();
    });
})();


// Preserve scroll position on form submission
document.addEventListener("submit", function (e) {
    if (e.target.matches("form")) {
        sessionStorage.setItem("scrollY", window.scrollY);
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const scrollY = sessionStorage.getItem("scrollY");
    if (scrollY) {
        window.scrollTo({ top: parseInt(scrollY, 10), behavior: "instant" });
        sessionStorage.removeItem("scrollY");
    }
});