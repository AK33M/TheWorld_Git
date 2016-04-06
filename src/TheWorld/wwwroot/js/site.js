//site.js
(function () {
    var $sidebarAndWrapper = $('#sidebar,#wrapper');

    $('#sidebarToggle').on('click', function () {
        $sidebarAndWrapper.toggleClass('hide-sidebar');
        if ($sidebarAndWrapper.hasClass('hide-sidebar')) {
            $(this).text('Show Sidebar');
        } else {
            $(this).text('Hide Sidebar');
        }
    });
    
    //var ele = $('#username');
    //ele.text("Akeem Taiwo");

    //var main = $('#main');
    //main.on('mouseenter', function () {
    //    main.style = 'background-color: #888;';
    //});

    //main.on('mouseleave', function () {
    //    main.style = '';
    //});

    //var menuItems = $('ul.menu li a');
    //menuItems.on('click', function () {
    //    var me = $(this);
    //    alert(me.text());
    //});
})();

