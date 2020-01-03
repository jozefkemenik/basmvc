var map;
$(document).ready(function() {

    /* ======= Flexslider ======= */
    $('.flexslider').flexslider({
        animation: "fade"
    });


    

    /* ======= jQuery Placeholder ======= */
    $('input, textarea').placeholder();
    
    
    /* ======= Carousels ======= */
    $('#news-carousel').carousel({interval: false});
    $('#videos-carousel').carousel({interval: false});
    $('#testimonials-carousel').carousel({interval: 6000, pause: "hover"});
    $('#awards-carousel').carousel({interval: false});
    
    
    /* ======= Flickr PhotoStream ======= */
    $('#flickr-photos').jflickrfeed({
        limit: 12,
        qstrings: {
        id: '32104790@N02' /* Use idGettr.com to find the flickr user id */
        },
        itemTemplate:
            '<li>' +
            '<a rel="prettyPhoto[flickr]" href="{{image}}" title="{{title}}">' +
            	'<img src="{{image_s}}" alt="{{title}}" />' +
            '</a>' +
            '</li>'
			
    }, function(data) {
    	$('#flickr-photos a').prettyPhoto();
    });
    
    /* ======= Pretty Photo ======= */
    // http://www.no-margin-for-errors.com/projects/prettyphoto-jquery-lightbox-clone/ 
    $('a.prettyphoto').prettyPhoto();
    
    /* ======= Twitter Bootstrap hover dropdown ======= */
    
    // apply dropdownHover to all elements with the data-hover="dropdown" attribute
    $('[data-hover="dropdown"]').dropdownHover();
    
    /* Nested Sub-Menus mobile fix */
    $('li.dropdown-submenu').on('click', function(event) {
        event.preventDefault(); 
        event.stopPropagation(); 
        $(this).find('.dropdown-menu').toggle();
    });       
    
    /* ======= Style Switcher ======= */
    
    $('#config-trigger').on('click', function(e) {
        var $panel = $('#config-panel');
        var panelVisible = $('#config-panel').is(':visible');
        if (panelVisible) {
            $panel.hide();        
        } else {
            $panel.show();
        }
        e.preventDefault();
    });
    
    $('#config-close').on('click', function(e) {
        e.preventDefault();
        $('#config-panel').hide();
    });
    
    
    $('#color-options a').on('click', function(e) { 
        var $styleSheet = $(this).attr('data-style');
        var $logoImage = $(this).attr('data-logo');
		$('#theme-style').attr('href', $styleSheet);
		$('#logo').attr('src', $logoImage);		
				
		var $listItem = $(this).closest('li');
		$listItem.addClass('active');
		$listItem.siblings().removeClass('active');
		
		e.preventDefault();
		
    });






});



var map;
jQuery(document).ready(function () {

    map = new GMaps({
        div: '#map',
        lat: 48.3300502,
        lng: 19.6630869,
    });
    map.addMarker({
        lat: 48.3300502,
        lng: 19.6630869,
        title: 'Adresa',
        infoWindow: {
            content: '<h5 class="title">Zakladna umelecka skola Lucenec</h5><p><span class="region">Novohradska 2</span><br><span class="postal-code">984 01 Lucenec</span><br><span class="country-name">Slovakia</span></p>'
        }

    });

});