//popover

//extends

//(function ($) {
//    $.fn.serializeTranslation = function () {
//        o = [];
//        var divitems = this.find("div.advitem");
//        divitems.each(function () {
//            var title = $(".item_Title", this).val();
//            var text = $(".item_Text", this).val();
//            var langId = $(".item_LangId", this).val();
//            o.push({ Title: title, Text: text, LangId: langId })
//        });
//        return o;
//    };
//})(jQuery);

//(function ($) {
//    $.fn.Translator = function (options) {
//        var settings = $.extend({
//            targetControlClassName: "",
//            notifyAction: function (isEnabled) { return false; }
//        }, options);

//        var translControl = this;
//        var sl = $(".lang-shortcut", "div.sl").val();

//        var sltext = translControl.find("div.sl");
//        sltext = $(".form-control." + settings.targetControlClassName, sltext);
//        var items = translControl.find("div.tab-pane:not(.sl)");

//        sltext.change(function (e) {
//            var requiredCountforTransl = 0;
//            var executedCountforTransl = 0;
//            settings.notifyAction(false);
//            var textval = e.target.value;
//            items.each(function (ix, element) {
//                var tltext = $(".form-control." + settings.targetControlClassName, this);
//                var islocked = tltext.hasClass("locked");
//                var tl = $(".lang-shortcut", this).val();
//                if (!islocked) {
//                    tltext.val('');
//                    tltext.valid();
//                    if (textval != '') {
//                        requiredCountforTransl++;
//                        Translate(textval, sl, tl, function (d) {
//                            tltext.val(d);
//                            tltext.valid();
//                            settings.notifyAction(++executedCountforTransl === requiredCountforTransl);

//                        });
//                    }
//                }
//            });
//        });

//        $("a." + settings.targetControlClassName, translControl).click(function () {
//            var div = $(this).parent();
//            var tl = $(".lang-shortcut", div.parent()).val();
//            var tltext = $(".form-control." + settings.targetControlClassName, div);
//            settings.notifyAction(false);
//            Translate(sltext.val(), sl, tl, function (d) {
//                tltext.val(d);
//                tltext.valid();
//                tltext.removeClass("locked");
//                settings.notifyAction(true);
//            });
//            return false;
//        });

//        items.each(function (ix, element) {
//            $(".form-control." + settings.targetControlClassName, this).keyup(function () {
//                $(this).addClass("locked");
//            })
//        });
//    };
//}(jQuery));
////***********************************************************************
//// GLOBAL VARIABLES TO REGISTER EVENTS ON NAVIGATION BUTTONS CLICK
//var CALLBACKNEXTBUTTONHOLDER = [];
//var CALLBACKPREVBUTTONHOLDER = [];

//function addCallbackNextBtn(callback) {
//    if (CALLBACKNEXTBUTTONHOLDER != null && CALLBACKNEXTBUTTONHOLDER.indexOf(callback) < 0) {
//        CALLBACKNEXTBUTTONHOLDER.push(callback);
//    }
//}

//function addCallbackPrevBtn(callback) {
//    if (CALLBACKPREVBUTTONHOLDER != null && CALLBACKPREVBUTTONHOLDER.indexOf(callback) < 0) {
//        CALLBACKPREVBUTTONHOLDER.push(callback);
//    }
//}
//function ResetCallbackHolders() {
//    CALLBACKNEXTBUTTONHOLDER = [];
//    CALLBACKPREVBUTTONHOLDER = [];

//}
//function Fire(e, i, list) {

//    if (list.length == 0) {
//        e();
//    }
//    else {
//        if (typeof list[i] == "function") {

//            if (i == list.length - 1) {
//                list[i](function () { e(); });
//            }
//            else {
//                list[i](function () { Fire(e, i + 1, list) });
//            }
//        }
//    }
//}

////FUNCTION CALLED AFTER CLICK NEXT
//function InvokeNextBtnEvent(e) {
//    Fire(e, 0, CALLBACKNEXTBUTTONHOLDER);
//}

////FUNCTION CALLED  AFTER CLICK PREV
//function InvokePrevBtnEvent(e) {
//    Fire(e, 0, CALLBACKPREVBUTTONHOLDER);
//}

//***********************************************************************

////FUNCTION TO TRANSLATE TEXT USING GOOGLE TRANSLATOR
//function Translate(txt, sl, lt, callback) {
//    var url = "/Translator/Translate";//'@Url.Action("Translate","Translator")';
//    var array = "";
//    $.ajax({
//        type: 'POST',
//        url: url,
//        data: { text: txt, fromLang: sl, toLang: lt },
//        dataType: 'json',
//        success: function (data) {
//            var jsn = JSON.parse(data);

//            $.each(jsn.sentences, function (index, d) {
//                array += d.trans;
//            });
//            callback(array, true);

//            //alert("Success: " + jsn.sentences);
//        },
//        error: function (jqXHR, textStatus, errorThrown) {
//            callback(textStatus, false);
//            //JSON.parse()
//        }
//    });
//}
////*********** Step Edit Form ...GoTo Category ****************
//function gotocategory(url, data, idhtml) {
//    $.ajax({
//        async: true,
//        url: url,
//        type: "GET",
//        data: data,
//        success: function (data) {
//            $("#" + idhtml).html(data);
//        }
//    });
//}
//function deleteAdvert(url, id, page, idhtml) {
//    $.ajax({
//        type: "POST",
//        async: true,
//        url: url,
//        data: { id: id, page: page },
//        success: function (data) {
//            $("#" + idhtml).html(data);
//        },
//        failure: function (errMsg) {
//        }
//    });
//    return false;
//    //ajaxPost('@Url.Action("Delete","Advertisement")', '{id=@item.Id}', 'navAdvList'); return false;
//}
////*********** Step Edit Form ...Translate Slider ****************
//function findSliderText(data, val) {

//    var result = '';
//    for (var i = 0; i < data.length; i++) {
//        if (data[i].val == val) {
//            result = data[i].text;
//            break;
//        }
//    };
//    return result;

//}

//function findEntry(entries, key) {

//    var result = null;
//    for (var i = 0; i < entries.length; i++) {
//        if (entries[i].key == key) {

//            result = entries[i].value;
//            break;
//        }
//    }

//    return result;
//}

//function ajaxPost(url, data, returnId) {
//    $.ajax({
//        async: true,
//        url: url,
//        type: "POST",
//        data: data,
//        success: function (result) {
//            $("#" + returnId).html(result);
//        }
//    });
//}
//function ajaxGet(url, data, returnId) {
//    $.ajax({
//        async: true,
//        url: url,
//        type: "GET",
//        data: data,
//        success: function (result) {
//            $("#" + returnId).html(result);
//        }
//    });
//}
//*********** Step Upload Image ****************



//function deleteThumContainer()
//{
//        $.ajax({
//            type: "POST",
//            url: this._crudSource,
//            data: JSON.stringify(this.data),
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            context: this,
//        })
//      .done(function (data) {

//          //if (this._callbackAfterCrud) {
//          //    var msg = JSON.parse(data.d);
//          //    this._callbackAfterCrud.call(this, msg);
//          //}
//      })
//      .fail(function (jqxhr, textStatus, error) {
//          //if (callbackAfterFail) {
//          //    callbackAfterFail.apply(this, [jqXHR, textStatus, error]);
//          //}
//      });
//}





















function deleteThumbnailContainer(id) {
    $.ajax({
        async: true,
        url: "/Files/DeleteImg",
        type: "POST",
        data: { id: id },
        success: function (result) {
            var item = $("#thumbnailContainer" + id);
            item.animate({ height: 1 },
            250,
            function () {
                item.remove();
                BrowseHideUpDownArrow();
                SetOnTopPicture();
            });
        }
    });
}
function SetOnTopPicture() {
    if (!$("input:radio[name='IsOnTopAlbum']").is(":checked")) {
        console.log($("input:radio[name='IsOnTopAlbum']:first"));
        $("input:radio[name='IsOnTopAlbum']:first").prop('checked', 'checked');
    }
}
//UPLOAD GENERAL FUNCTION
function UploadImage() {
    $('#fileupload').fileupload({
        dataType: "json",
        url: "/Upload/UploadFile",
        // formData: { albumId: albumId },
        limitConcurrentUploads: 1,
        sequentialUploads: true,
        progressInterval: 100,
        maxFileSize: 5000000,
        //limitConcurrentUploads: 1,
        //progressInterval: 100,
        //maxChunkSize: 10000,
        add: function (e, data) {
            var node = $('<div class="thumbnailContainer well "></div>').appendTo('#filelistholder');
            data.context = $('<div class="row"></div>').appendTo(node);
            $('<div class="col-lg-12 content"><div class="progress"><div class="progress-bar bar" style="width:0%"></div></div></div>').appendTo(data.context);
            data.submit();
        },
        start: function (e) {
            $('#overallbar').css('width', 0 + '%');
        },
        submit: function (e, data) {
            var $this = $(this);
        },

        done: function (e, data) {
            setTimeout(function () {
                var ulnode = $("ul.media-list");
                var str = "<li id='thumbnailContainer" + data.result.imageid + "' class='thumbnailContainer well media ui-sortable-handle'>" + data.result.content + "</li>"
                ulnode.append(str);
                data.context.parent().addClass('hidden');
                //data.context.parent().attr("id", "thumbnailContainer" + data.result.imageid);
                //data.context.parent().html(data.result.content);
                BrowseHideUpDownArrow();
                SetOnTopPicture();
                //DoSortable();

            }, 600);
        },
        progressall: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $('#overallbar').css('width', progress + '%');
        },
        progress: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            data.context.find('.bar').css('width', progress + '%');
        }
    });
}

// SAVE DESCRIPTION TEXT
function SaveFileDesc(e) {
    var data = [];
    $(".thumbnailContainer").each(function (ix, element) {
        var id = $('input[name=fileid]', this).val();
        var desc = $('.description', this).val();
        var isontop = false;
        if ($('.isontop', this).prop('checked'))
        { isontop = true; }
        data.push({ Id: id, Description: desc, IsOnTopAlbum: isontop });
    });
    $.ajax({
        type: "POST",
        url: "/Files/SaveDesc",

        data: "{'jsonstring':'" + JSON.stringify(data) + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            e();
        },
        failure: function (errMsg) {
        }
    });
};

//OBSOLOTE
function ShowTargetHtml($target) {
    var $clone = $target.clone();
    $clone.wrap('<div>');
    var htmlString = $clone.parent().html();
    $('#results').val(htmlString);
    $('#results').val($target.clone().wrap('<div>').parent().html());
}

function moveUp(item) {
    var prev = item.prev();
    if (prev.length == 0)
        return;
    prev.css('z-index', 999).css('position', 'relative').animate({ top: item.height() }, 250);
    item.css('z-index', 1000).css('position', 'relative').animate({ top: '-' + prev.height() }, 300, function () {
        prev.css('z-index', '').css('top', '').css('position', '');
        item.css('z-index', '').css('top', '').css('position', '');
        item.insertBefore(prev);
        BrowseHideUpDownArrow()
    });
}
function moveDown(item) {
    var next = item.next();
    if (next.length == 0)
        return;
    next.css('z-index', 999).css('position', 'relative').animate({ top: '-' + item.height() }, 250);
    item.css('z-index', 1000).css('position', 'relative').animate({ top: next.height() }, 300, function () {
        next.css('z-index', '').css('top', '').css('position', '');
        item.css('z-index', '').css('top', '').css('position', '');
        item.insertAfter(next);
        BrowseHideUpDownArrow();
    });
}

function BrowseHideUpDownArrow() {
    var containerNodes = $(".thumbnailContainer")
    var length = containerNodes.length;
    containerNodes.each(function (index, node) {
        if (index == 0) {
            $(".button-up", this).addClass("hidden");
        }
        else {
            $(".button-up", this).removeClass("hidden");
        }
        if (index == length - 1) {
            $(".button-down", this).addClass("hidden");;
        }
        else {
            $(".button-down", this).removeClass("hidden");
        }
    });
}
// region Country picker ----------------------------------------------
function browsetooglemenu() {

    $('span.tooglemenu').click(function () {
        var node = $(this);
        var isfirst = node.hasClass('first');
        var inode = $("i", this);
        if (inode.hasClass('fa-close')) {
            node.prev().autocomplete('close');
        }
        else
            if (inode.hasClass('fa-chevron-circle-down')) {
                if (isfirst) {
                    node.prev().data("ui-autocomplete").search("");
                }
                else {
                    node.prev().data("ui-autocomplete").search(this.term);
                }

            }

    });
}

function udpateArrowCloseIcon(target, show) {
    var inode = $(target).next().children();
    if (show) {
        inode.removeClass('fa-chevron-circle-down');
        inode.addClass('fa-close');

    }
    else {
        inode.removeClass('fa-close');
        inode.addClass('fa-chevron-circle-down');
    }
}
// endregion

function post_to_url(url, params) {
    var form = document.createElement('form');
    form.action = url;
    form.method = 'POST';

    for (var i in params) {
        if (params.hasOwnProperty(i)) {
            var input = document.createElement('input');
            input.type = 'hidden';
            input.name = i;
            input.value = params[i];
            form.appendChild(input);
        }
    }
    document.body.appendChild(form);
    form.submit();
    return false;
}

function pagingClick(url, wrapid, page, pagesize, isnext) {
    if (isnext) {
        page++;
    }
    else {
        page--;
    }
    $.ajax({
        async: true,
        url: url,
        type: "GET",
        data: { page: page, pagesize: pagesize },
        success: function (result) {
            $("#" + wrapid).html(result);
        }
    });
    return false;
}
//function initAutocomplete(target, valueId, source) {
//    target.autocomplete({
//        delay: 0,
//        minLength: 3,
//        autoFocus: true,
//        source: source,

//        position: {
//            my: "left bottom",
//            at: "left top",
//            collision: "flip"
//        },
//        create: function () {

//            var data = $(this).data('ui-autocomplete');

//            data._renderItem = function (ul, item) {
//                return $('<li>')
//                    .append('<a>' + item.Name + '</a>')
//                    .appendTo(ul);
//            };
//        },
//        select: function (event, ui) {
//            event.target.value = ui.item.Name;
//            valueId.val(ui.item.Id);
//            return false;
//        },

//        focus: function (event, ui) {
//            if (valueId.val() > -1) {
//                event.target.value = '';
//                valueId.val(-1);
//            }
//        }
//    ,
//        open: function (event, ui) {
//            var inode = $(event.target).next().children();
//            udpateArrowCloseIcon(inode, true);
//        },
//        close: function (event, ui) {
//            var inode = $(event.target).next().children();
//            udpateArrowCloseIcon(inode, false);
//        }
//    });

//}

//var url = "http://translate.google.com/translate_a/single?client=t&sl=en&tl=sk&dt=t&ie=UTF-8&oe=UTF-8&q=good";

//var url = "http://translate.google.com/translate_a/t?client=p&text=This is good day. Nice day is today.&langpair=sv%7Cen";

//result = [[NSString alloc] initWithData:myData encoding: NSUTF8StringEncoding];

//$.get(url, function (data) {
//    alert("Data: " + data);
//});

//var success = function(data){
//    var html = [];
//    /* parse JSON */
//    data = $.parseJSON(data);
//    /* loop through array */
//    $.each(data, function(index, d){           
//        html.push("Manufacturer : ", d.Manufacturer, ", ",
//                  "Sold : ", d.Sold, ", ",
//                  "Month : ", d.Month, "<br>");
//    });
//function loadData(data) {
//    var x = JSON.parse(data);
//    alert(x);
//    $.each(data, function (i, doc) {
//        alert('aaa');
//        // $(“#results”).append(‘Title:’+doc.name+’ == Price:’+doc.price+”);
//    });
//}
//$.ajaxSetup({
//    crossDomain: true,
//    scriptCharset: 'utf-8',
//    contentType: 'jsonp; charset=utf-8',
//    jsonp: 'callback',
//    jsonpCallback: loadData
//});

//$.ajax({
//    type: 'GET',  
//    url: 'http://translate.google.com/translate_a/t',
//    data: { client: "t", text: 'doma', sl: 'sk', tl: 'en' },
//    dataType: 'jsonp', 

//    cache: false,
//    success: function (data) {
//        alert("Success");
//    },
//    error:function(jqXHR, textStatus, errorThrown){
//        alert(textStatus);
//        //JSON.parse()
//    }
//});