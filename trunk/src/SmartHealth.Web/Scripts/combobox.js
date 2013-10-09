jQueryselectDroplist_Manager = new
function() {
    this.els = [];
    this.activeName = null;
    return this;
}
jQueryselectDroplist_UI = function(jEl, options) {
    var o = this;
    this.setupDropListUI = function() {
        var offset = 0;
        o.select.find("> *").each(function(index) {
            var el = jQuery(this);
            var count = index;
            if (this.tagName.toLowerCase() == "optgroup") {
                el.each(function() {
                    var optgroup = jQuery(this);
                    var optName = optgroup.attr("label");
                    var optgroup_el = jQuery("<li></li>");
                    optgroup_el.prepend("<span class=\"OptgroupLabel\">" + optName + "</span>");
                    var optgroup_elsubUL = jQuery("<ul></ul>");
                    o.elUL.append(optgroup_el);
                    optgroup_el.append(optgroup_elsubUL);
                    optgroup.find("option").each(function(index) {
                        console.log(self.attr("selected"));
                        var self = jQuery(this);
                        if (self.attr("value") == "") {
                            optgroup_elsubUL.append("<li class=\"SelectUITitle\" value=\"" + parseInt(count + index + offset + 1) + "\"><a href=\"#\" title=\"" + self.text() + "\" rel=\"" + self.attr("label") + "\">" + self.text() + "</a></li>");
                        }
                        else if (self.attr("selected")) {
                            optgroup_elsubUL.append("<li class=\"Active\" value=\"" + parseInt(count + index + offset + 1) + "\"><a href=\"#\" title=\"" + self.text() + "\" rel=\"" + self.attr("label") + "\">" + self.text() + "</a></li>");
                        }
                        else {
                            optgroup_elsubUL.append("<li value=\"" + parseInt(count + index + offset + 1) + "\"><a href=\"#\" title=\"" + self.text() + "\" rel=\"" + self.attr("label") + "\">" + self.text() + "</a></li>");
                        }
                    });
                    offset += optgroup.find("option").length - 1;
                });
            }
            else {
                if (el.attr("value") == "") {
                    o.elUL.append("<li class=\"SelectUITitle\" value=\"" + parseInt(index + offset + 1) + "\"><a href=\"javascript:void();\" title=\"" + el.text() + "\" rel=\"" + el.attr("label") + "\">" + el.text() + "</a></li>");
                }
                else if (el.attr("selected")) {
                    o.elUL.append("<li class=\"Active\" value=\"" + parseInt(index + offset + 1) + "\"><a href=\"javascript:void();\" title=\"" + el.text() + "\" rel=\"" + el.attr("label") + "\">" + el.text() + "</a></li>");
                }
                else {
                    o.elUL.append("<li value=\"" + parseInt(index + offset + 1) + "\"><a href=\"javascript:void();\" title=\"" + el.text() + "\" rel=\"" + el.attr("label") + "\">" + el.text() + "</a></li>");
                }
            }
        });
        //o.el.html(o.elUL);
        var elClasses = o.elUL.attr("class").split(" ");
        var addDefaultTheme = true;
        for (var i = 0; i < elClasses.length; i++) {
            if (elClasses[i].match(/^Theme/)) {
                o.elWrapper.addClass(elClasses[i]);
                addDefaultTheme = false;
            }
        }
        if (addDefaultTheme) {
            o.elWrapper.addClass("Theme_Default");
            o.elUL.addClass("Theme_Default");
        }
        if (!o.select.attr("multiple")) {
            o.maxDropListHeight = options != undefined && options.maxDropListHeight != undefined ? parseInt(options.maxDropListHeight) : 124;
            o.config = {
                maxDropListHeight: o.maxDropListHeight
            }
            var title = "";
            var hasValue = false;
            o.select.find("option").each(function() {
                var option = jQuery(this);
                if (option.attr("selected")) {
                    title = option.text();
                    hasValue = true;
                }
            });
            if (!hasValue) {
                title = o.select.attr("title") != "" ? o.select.attr("title") : o.select.find("option:first-child").text();
            }
            if (!o.select.attr("disabled")) {
                o.droplistTITLE.text(title);
                o.elWrapper.removeClass("Disabled");
            }
            else {
                o.droplistTITLE.text(title);
                o.elWrapper.addClass("Disabled");
            }
            o.el.show();
            o.el.css({
                position: "absolute",
                left: 0,
                display: "none",
                overflow: "hidden",
                width: o.elUL.width() + 2
            });
            o.el.hide();
            o.el.find("ul > li").each(function(index) {
                var self = jQuery(this);
				
				if(index == 1) {
					self.addClass("First");
				}
				
                self.bind("click", function() {
                    if (self.find("span.OptgroupLabel:first-child").length > 0) {
                        return false;
                    }
                    else {
                        if (!o.select.attr("disabled")) {
                            o.el.find("ul > li").removeClass("Active");
                            self.addClass("Active");
                            o.droplistTITLE.text(self.text());
							o.droplistInput.hide();
                            o.hideList();	
                            o.select.val(o.select.find("option").eq(self.attr("value") - 1).val());
                            callExternalFunction(o, jQueryselectDroplist_Manager.els, self.find("a:first").attr("rel"));
                            self.removeClass("Hover");
                            o.afterCall();
                            return false;
                        }
                    }
                });
            });
            o.el.bind("click", function(evt) {
                return false;
            });
        }
        else {
            var size = o.select.attr("size");
            var _height = 0;
           
            if (!o.elUL.parent().hasClass("jspContainer")) {
                o.elUL.parent().jScrollPane({
                    verticalGutter : o.options.verticalGutter,
                    scrollbarOnLeft: o.options.scrollbarSide == "left" ? true : false
                });
            }
            var keyChar = null;
            var beginVal_INDEX = null;
            var endVal_INDEX = null;

            function clearValues() {
                o.select.find("option").removeAttr("selected");
                o.elUL.find("li").removeClass("Active");
            }
            o.el.find("ul > li").each(function(index) {
                var self = jQuery(this);
                self.bind("click", function(e) {
                    if (self.find("span.OptgroupLabel:first-child").length > 0) {
                        return false;
                    }
                    else {
                        if (!o.select.attr("disabled")) {
                            if (e.ctrlKey && !e.shiftKey) { 
                                beginVal_INDEX = index;
                                o.select.find("option").eq(index).attr("selected", "selected");
                            }
                            else if ((!e.ctrlKey && e.shiftKey) || (e.ctrlKey && e.shiftKey)) {
                                if (!e.ctrlKey) {
                                    clearValues();
                                }
                                if (beginVal_INDEX == null) {
                                    beginVal_INDEX = index;
                                }
                                else {
                                    endVal_INDEX = index;
                                    if (beginVal_INDEX != null && endVal_INDEX != null) {
                                        o.el.find("ul > li").each(function(index) {
                                            var self = jQuery(this);
                                            if ((beginVal_INDEX <= endVal_INDEX && index >= beginVal_INDEX && index <= endVal_INDEX) || (beginVal_INDEX >= endVal_INDEX && index <= beginVal_INDEX && index >= endVal_INDEX)) {
                                                o.select.find("option").eq(index).attr("selected", "selected");
                                                self.addClass("Active");
                                            }
                                        });
                                        endVal_INDEX = null;
                                    }
                                }
                            }
                            else {
                                clearValues();
                                o.select.find("option").eq(index).attr("selected", "selected");
                                beginVal_INDEX = index;
                            }
                            self.addClass("Active");
                            self.removeClass("Hover");
                            return false;
                        }
                    }
                });
            });
        }
        o.el.find("ul > li").each(function(index) {
            var self = jQuery(this);
            self.bind("mouseover", function() {
                self.addClass("Hover");
                return false;
            });
            self.bind("mouseout", function() {
                self.removeClass("Hover");
                return false;
            });
        });
    }
    this.reset = function() {
        o.elUL.empty();
        o.elUL.removeAttr("class");
        o.elUL.attr("title", o.select.attr("title"));
        o.elUL.addClass(o.select.attr("class"));
        this.setupDropListUI();
    }
    this.showList = function() {
        o.elWrapper.addClass("TopLevel");
        o.el.addClass("DropListUIShow");
        o.reservedHolder = o.elWrapper.clone(true).empty();

        o.reservedHolder.css({
            visibility: "hidden",
            height: o.elWrapper.outerHeight()
        });
        o.elWrapper.before(o.reservedHolder);
        var borderLeftWidth = 0;
        var borderTopWidth = 0;
        var offset = {
            left: 0,
            top: 0
        };
        o.elWrapper.hide();
        o.elWrapper.appendTo("body");
        if(navigator.userAgent.match(/OS 3/i)) {
			offset.top = o.reservedHolder.offset().top - jQuery(window).scrollTop();	
		}
		else {
			offset.top = o.reservedHolder.offset().top;	
		}

        offset.left = o.reservedHolder.offset().left;
        o.elWrapper.css({
            position: "absolute",
            top: offset.top + borderTopWidth,
            left: offset.left + borderLeftWidth,
            margin: 0
        });
        o.elWrapper.show();
        o.el.show();
        o.setDirection();
            if (!o.elUL.parent().hasClass("jspContainer")) { 
                o.elUL.parent().jScrollPane({
                    verticalGutter: o.options.verticalGutter,
                    scrollbarOnLeft: o.options.scrollbarSide == "left" ? true : false
                });
            }		       
        o.eventFire = false;		
    }
    this.hideList = function() {
        o.el.prepend(o.elUL);
        o.elUL.removeAttr("style");
        o.elUL.next().remove();
		o.elULWrap.html(o.elUL);
		o.el.html(o.elULWrap);
        o.elWrapper.removeClass("TopLevel");
        o.el.removeClass("DropListUIShow");
        o.select.after(o.elWrapper.removeAttr("style"));
        o.el.hide();
        if (o.reservedHolder != null) {
            o.reservedHolder.remove();
        }			
		
    }	
    this.afterCall = function() { 
        if (options.afterAction != undefined) {
            options.afterAction();
        }
    }
	this.onAfterInit = function() {
		 if (options.onAfterInit != undefined) {
            options.onAfterInit();
        }
	}
    this.setDirection = function() {
        var windowHeight = jQuery.browser.safari ? window.innerHeight : jQuery(window).height();
        windowHeight = windowHeight + jQuery(window).scrollTop();
        var elPostion_Top = o.elWrapper.offset().top;
        var elPostion_Bottom = o.elWrapper.offset().top + o.elWrapper.height();
        var elULHeight = o.elUL.outerHeight();
        var direction = "";
        if (o.config.maxDropListHeight > o.maxDropListHeight) {
            o.maxDropListHeight = o.config.maxDropListHeight;
        }
        if (elULHeight <= windowHeight - elPostion_Bottom) {
            direction = "down";
        }
        else if (windowHeight - elPostion_Bottom > o.maxDropListHeight) {
            direction = "down";
        }
        else if (elULHeight < elPostion_Top - jQuery(window).scrollTop()) {
            direction = "up";
        }
        else if (elPostion_Top - jQuery(window).scrollTop() > o.maxDropListHeight) {
            direction = "up";
        }
        else if (windowHeight - elPostion_Bottom >= elPostion_Top - jQuery(window).scrollTop()) {
            direction = "down";
            o.maxDropListHeight = windowHeight - elPostion_Bottom;
        }
        else {
            direction = "up";
            o.maxDropListHeight = elPostion_Top - jQuery(window).scrollTop();
        }
        var borderTop = (/[0-9]+/).test(o.el.css("borderTopWidth")) ? parseInt(o.el.css("borderTopWidth")) : 0;
        var borderBottom = (/[0-9]+/).test(o.el.css("borderBottomWidth")) ? parseInt(o.el.css("borderBottomWidth")) : 0;
        o.maxDropListHeight -= (borderTop + borderBottom);
        if (direction == "up") {
            o.el.css({
                bottom: o.elWrapper.height() + 0 + "px",
                top: "auto"
            });
            o.el.addClass("DropListUIContainerUp");
            o.el.removeClass("DropListUIContainer");
        }
        else {
            o.el.css({
                top: "100%",
                bottom: "auto"
            });
            o.el.addClass("DropListUIContainer");
            o.el.removeClass("DropListUIContainerUp");
        }
    }
    o.options = {
        verticalGutter: options != undefined && options.verticalGutter != undefined ? parseInt(options.verticalGutter) : 10,
        scrollbarSide: options != undefined && options.scrollbarSide != undefined ? options.scrollbarSide : "right",
		isFilter: options != undefined && options.isFilter != undefined ? options.isFilter : false,
		onAfterInit: function(){}
    }
    o.select = jEl;
    o.select.addClass("HasSelectUI");
    o.select.css({
        opacity: 0,
        position: "absolute",
        left: "-1000em",
        top: "0em"
    });
    o.reservedHolder = null;
    o.elUL = jQuery("<ul title=\"" + o.select.attr("title") + "\"></ul>");
    o.elUL.addClass(o.select.attr("class"));
    o.select.before(o.elUL);
    o.el = jQuery("<div class=\"DropListUIContainer\"></div>");
	o.elULWrap = jQuery("<div class=\"ScrollWrap\"></div>");    
    o.elUL.before(o.el);	
	o.elUL.before(o.elULWrap);	
	o.elULWrap.html(o.elUL);
	o.el.html(o.elULWrap);
    o.elWrapper = jQuery("<div class=\"DropListUI\"></div>");
    o.el.before(o.elWrapper);
    o.elWrapper.html(o.el);
	
	o.droplistInput = jQuery("<input id=\"filter\" type=\"text\" class='DropListInput'></input>");		
	o.el.before(o.droplistInput);
	o.droplistInput.hide();
	
    if (!o.select.attr("multiple")) {		
        o.droplistTITLE = jQuery("<p></p>");		
        o.el.before(o.droplistTITLE);
		if(options.isFilter){
			o.droplistArrow = jQuery("<span class=\"Arrow\"></span>");		
			o.el.before(o.droplistArrow);		
			o.droplistArrow.bind("click", function(evt) {												   
				if (options.beforeAction != undefined) {
					options.beforeAction();
				}
				if (!o.select.attr("disabled")) {
					if (o.el.hasClass("DropListUIShow")) {
						o.hideList();
					}
					else {
						if (jQueryselectDroplist_Manager.activeName != null) {
							jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName].hideList();
						}					
						o.showList();
						jQueryselectDroplist_Manager.activeName = o.select.attr("id");
					}
					jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName].eventFire = true;
				}			 
				return false;
			});
			o.droplistTITLE.bind("click", function(evt){
				o.hideList();		
				o.droplistInput.show();
				o.droplistInput.val("");
				o.droplistInput.focus();
			})
		}
		else {/*o.droplistTITLE.focus(function(){alert("asds")})*/
			o.droplistTITLE.bind("click", function(evt){
				if (options.beforeAction != undefined) {
					options.beforeAction();
				}
				if (!o.select.attr("disabled")) {
					if (o.el.hasClass("DropListUIShow")) {
						o.hideList();
					}
					else {
						if (jQueryselectDroplist_Manager.activeName != null) {
							jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName].hideList();
						}					
						o.showList();
						jQueryselectDroplist_Manager.activeName = o.select.attr("id");
					}
					jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName].eventFire = true;
				}			 
				return false;
			})
		}
    }
	else {
		o.elWrapper.addClass("Multiple");
		o.el.addClass("MultipleContainer");		
	}
    this.setupDropListUI();
}
jQuery.fn.extend({
    addSelectUI: function() {
        if (jQueryselectDroplist_Manager != undefined) {
            jQuery(window).bind("resize", function(evt) {
                if (jQueryselectDroplist_Manager.activeName != null && jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName] != undefined && !jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName].eventFire) {
                    jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName].hideList();
                }
            });
            jQuery(document).bind("click", function(evt) {									
                if (jQueryselectDroplist_Manager.activeName != null) {
                    jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName].hideList();
                }
            });
            jQuery(window).bind("scroll", function(evt) {
                if (jQueryselectDroplist_Manager.activeName != null && jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName] != undefined && !jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName].eventFire) { 
                    jQueryselectDroplist_Manager.els[jQueryselectDroplist_Manager.activeName].hideList();
                }
            });
        }
        var options = arguments[0];
        this.each(function() {
            if (!jQuery(this).hasClass("HasSelectUI")) {
                jQuery(this).addClass("HasSelectUI");
                jQueryselectDroplist_Manager.els[jQuery(this).attr("id")] = new jQueryselectDroplist_UI(jQuery(this), options);				
				jQueryselectDroplist_Manager.els[jQuery(this).attr("id")].onAfterInit();
            }
        });
    }
});

/* jSelect (using jQuery library).
 *--------------------------------------------*
 *  @author : ukhome ( ukhome@gmail.com | ntkhoa_friends@yahoo.com )
 *--------------------------------------------*
 *  @released : 24-Mar-2009 : version 1.0
 *--------------------------------------------*
 *  @revision history : ( latest version : 1.0 )
 *--------------------------------------------*
 *      + 24-Mar-2009 : version 1.0
 *          - released
 *--------------------------------------------*
 */

/* External Interface
 */

function callExternalFunction(o, droplists, val) {
	/*
	 * o : selectUI object o.select : <select> in jQuery type o.elUL : list drop
	 * down, main list <ul> ----------------------------------------------*
	 * droplists : all selectUI droplists in page, call by its id droplists(id),
	 * will return selectUI object val : rel value in a of each selectUI option
	 */

	/* Example: 
	var getSelectValue = o.select.val();
	var getSelectId = o.select.attr('id');	
	*/

    if (o.select[0].click)
        o.select[0].click();
}