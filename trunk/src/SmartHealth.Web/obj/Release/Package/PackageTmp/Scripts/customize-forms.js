// Show hide label
jQuery(".DefaultLabel").each(function() {
	if(jQuery(this).val() == "") {
		jQuery(this).siblings(".DefaultLabelContent").show();
	} else {
		jQuery(this).siblings(".DefaultLabelContent").hide();
	}
	jQuery(this).focus(function() {
		jQuery(this).siblings(".DefaultLabelContent").hide();
	}).blur(function() {
		if(jQuery(this).val() == "") {
			jQuery(this).siblings(".DefaultLabelContent").show();
		}
	});
});
/* FILE BROWSE OBJECT */
/*
 *FileBrowseUI will be created to replace the original file browse
 *FileBrowseUI will be created like this :
 <div>									:	Wrapper for all elements
 <input type="text" />				:	Fake Text Input to show the browse path
 ID and Name of the origin browse will be copy to this
 textbox and remove them on the original browse
 <div><input type="file" /></div>	:	<div> = wrapper for original file browse
 </div>
 *Options :
 browseButtonPos			:	set the Browse button position
 left/right then top/bottom(affected when breakline==TRUE)
 default: "right"
 breakline				:	if set to TRUE, textbox and Browse button are on 2 line
 (top/bottom available)
 browseButtonClassName	:	set the classname for Browse button, default = DefaultBrowseBtn
 browseTextBoxClassName	:	set the classname for the TextBox, default = DefaultBrowseTextBox
 */
FileBrowseUIGroup = function() {
	this.FileBrowseUIGroup = getElementsByClassName(arguments[0]);
	for(var i = 0; i < this.FileBrowseUIGroup.length; i++) {
		new FileBrowseUI(this.FileBrowseUIGroup[i], arguments[1]);
	}
}
FileBrowseUI = function() {
	this.options = {
		browseButtonPos : arguments[1] && arguments[1].browseButtonPos ? arguments[1].browseButtonPos : "right",
		browseButtonClassName : arguments[1] && arguments[1].browseButtonClassName ? arguments[1].browseButtonClassName : "DefaultBrowseBtn",
		browseTextBoxClassName : arguments[1] && arguments[1].browseTextBoxClassName ? arguments[1].browseTextBoxClassName : "DefaultBrowseTextBox",
		breakline : arguments[1] && arguments[1].breakline ? arguments[1].breakline : false,
		browseButtonText : arguments[1].browseButtonText ? arguments[1].browseButtonText : "Browse",
		browseTextBoxValue : arguments[1].browseTextBoxValue ? arguments[1].browseTextBoxValue : ""

	}
	//Left/Right + Top/Bottom
	var lr = this.options.browseButtonPos.split(" ")[0];
	var tb = this.options.browseButtonPos.split(" ")[1];
	//Original file browse
	this.originalFileBrowse = arguments[0];
	//Create file browse wrapper
	var inputWrapper = document.createElement("div");
	this.originalFileBrowse.parentNode.insertBefore(inputWrapper, this.originalFileBrowse);
	inputWrapper.appendChild(this.originalFileBrowse);

	//Create fake input text for fake Browse...
	var fakeTextBoxWrapper = document.createElement("div");
	this.fakeTextBoxInput = document.createElement("input");
	this.fakeTextBoxInput.id = "fake_" + this.originalFileBrowse.id;
	//this.originalFileBrowse.id = "";
	this.fakeTextBoxInput.name = "fake_" + this.originalFileBrowse.name;
	//this.originalFileBrowse.name = "";
	inputWrapper.parentNode.insertBefore(fakeTextBoxWrapper, inputWrapper);
	fakeTextBoxWrapper.appendChild(this.fakeTextBoxInput);

	//Create BrowseUI Wrapper for al element
	var browseUIWrapper = document.createElement("div");
	inputWrapper.parentNode.insertBefore(browseUIWrapper, inputWrapper);
	//Append to Browser
	if( typeof (tb) == "undefined" || tb == "bottom") {
		browseUIWrapper.appendChild(fakeTextBoxWrapper);
		browseUIWrapper.appendChild(inputWrapper);
	} else {
		browseUIWrapper.appendChild(inputWrapper);
		browseUIWrapper.appendChild(fakeTextBoxWrapper);
	}
	browseUIWrapper.className = this.originalFileBrowse.className;

	//Prepare styling...
	browseUIWrapper.style.overflow = "hidden";
	//Browse button
	inputWrapper.style.position = "relative";
	inputWrapper.style.overflow = "hidden";
	this.originalFileBrowse.style.fontSize = "10em";
	this.originalFileBrowse.style.position = "absolute";
	this.originalFileBrowse.style.Zindex = "1";
	this.originalFileBrowse.style.top = "0px";
	this.originalFileBrowse.style.right = "0px";
	this.originalFileBrowse.className = "";
	inputWrapper.className += this.options.browseButtonClassName;

	//Start Modify
	var textBrowse = document.createElement("div");
	inputWrapper.appendChild(textBrowse);
	textBrowse.innerHTML = this.options.browseButtonText;
	//End Modify
	//Event: onmousedown state
	inputWrapper.onmousedown = function() {
		if(jQuery("#DiaryLinkType").length) {
			jQuery("#DiaryLinkType").val('Photo');
		}
		inputWrapper.className += " FileBrowseMouseDown";
	}
	inputWrapper.onmouseout = function() {
		inputWrapper.className = inputWrapper.className.replace(/FileBrowseMouseDown/, "");
	}
	var ie = document.all;
	if(ie) {
		inputWrapper.style.styleFloat = lr;
		this.originalFileBrowse.style.filter = "alpha(opacity=0)";
	} else {
		inputWrapper.style.cssFloat = lr;
		this.originalFileBrowse.style.opacity = 0;
		this.originalFileBrowse.style.MozOpacity = 0;
	}
	//Browse TextBox: this.fakeTextBoxInput
	fakeTextBoxWrapper.className += "TextBoxWrapper";
	this.fakeTextBoxInput.className += this.options.browseTextBoxClassName;
	this.fakeTextBoxInput.style.margin = "0px";
	this.fakeTextBoxInput.style.outline = "none";
	this.fakeTextBoxInput.value = this.options.browseTextBoxValue;
	if(ie) {
		if(lr == "left") {
			fakeTextBoxWrapper.style.styleFloat = "right";
		} else {
			fakeTextBoxWrapper.style.styleFloat = "left";
		}
		this.fakeTextBoxInput.style.marginTop = "-1px";
		/*fix for IE*/
	} else {
		if(lr == "left") {
			fakeTextBoxWrapper.style.cssFloat = "right";
		} else {
			fakeTextBoxWrapper.style.cssFloat = "left";
		}
		fakeTextBoxWrapper.style.width = this.fakeTextBoxInput.offsetWidth + "px";
		/*fix for MAC Safari not effected others*/
	}
	//browseUIWrapper dimension
	browseUIWrapper.style.width = this.options.breakline ? this.fakeTextBoxInput.offsetWidth + "px" : inputWrapper.offsetWidth + this.fakeTextBoxInput.offsetWidth + 6 + "px";
	//Event
	var self = this;
	this.originalFileBrowse.onchange = function() {
		self.updatedFileBrowseUI();
	}
	/*method*/
	//Update FileBrowseUI
	FileBrowseUI.prototype.updatedFileBrowseUI = function() {
		this.fakeTextBoxInput.value = this.originalFileBrowse.value;
	}
}
/* Misc functions */
function getElementsByClassName(className) {
	var children = document.getElementsByTagName('*') || document.all;
	var elements = new Array();

	for(var i = 0; i < children.length; i++) {
		var child = children[i];
		var classNames = child.className.split(' ');
		for(var j = 0; j < classNames.length; j++) {
			if(classNames[j] == className) {
				elements.push(child);
				break;
			}
		}
	}
	return elements;
}

//Cancel Bubling
function stopEventBubling(e) {
	if(!e)
		e = window.event;
	if(e.stopPropagation) {
		e.stopPropagation();
	} else {
		e.cancelBubble = true;
	}
}

//Change CSS style to DOM style
function DOMStyle(styleProp) {
	parseString = styleProp.split("-");
	styleProp = "";
	for(var i = 1; i < parseString.length; i++) {
		parseString[i] = parseString[i].replace(parseString[i].charAt(0), parseString[i].charAt(0).toUpperCase());
	}
	for(var i = 0; i < parseString.length; i++) {
		styleProp += parseString[i];
	}
	return styleProp;
}

function getStyle(element, styleProp) {
	if(element.currentStyle) {
		styleProp = DOMStyle(styleProp);
		var value = element.currentStyle[styleProp];
	} else {
		if(window.getComputedStyle) {
			var value = document.defaultView.getComputedStyle(element, null).getPropertyValue(styleProp);
		}
	}
	return value;
}

function findAbsPos(obj) {
	var curleft = curtop = 0;
	if(obj.offsetParent) {
		do {
			curleft += obj.offsetLeft;
			curtop += obj.offsetTop;
		} while (obj = obj.offsetParent);
	}
	return {
		left : curleft,
		top : curtop
	};
}

jQuery.fn.extend({
	addCheckBoxUI : function(cB, aC) {
		//checkboxUI Class
		$checkboxUI = function(jEl) {
			var self = this;
			this.checkbox = jEl;
			// el = jQuery Obj
			this.label = jEl.next();

			if(this.label[0] != null && this.label[0].nodeName != 'LABEL' && this.label[0].nodeName != 'label') {
				this.label = jEl.next().next();
			}

			//pre-processor: check whether when page load,
			//the checkbox is Checked or Not
			if(self.checkbox.prop("checked") == true) {
				self.label.addClass(aC);
			} else {
				self.label.removeClass(aC);
			}

			this.label.bind("click", function(evt) {
				//alert(!self.checkbox.attr("checked"))
				//console.log(self.checkbox.prop("checked"));
				if(!self.checkbox.prop("checked") == true && !self.checkbox.prop("disabled")) {

					self.label.addClass(aC);
				} else {
					self.label.removeClass(aC);
				}
				evt.stopPropagation();
			});
			cB(this.checkbox, this.label);
		}
		//setup
		this.each(function() {
			new $checkboxUI($(this));
		});
	},
	
	//fixbug checkbox no use label for 12/06/2012 
	addCheckBoxUI2 : function(cB, aC) {
		//checkboxUI Class
		$checkboxUI = function(jEl) {
			var self = this;
			this.checkbox = jEl;
			// el = jQuery Obj
			this.label = jEl.next();

			if(this.label[0] != null && this.label[0].nodeName != 'LABEL' && this.label[0].nodeName != 'label') {
				this.label = jEl.next().next();
			}

			//pre-processor: check whether when page load,
			//the checkbox is Checked or Not
			if(self.checkbox.prop("checked") == true) {
				self.label.addClass(aC);
			} else {
				self.label.removeClass(aC);
			}

			this.label.bind("click", function(evt) {
				//console.log(self.checkbox.prop("checked"));
				if(!self.checkbox.prop("checked") == true && !self.checkbox.prop("disabled")) {
					self.checkbox.prop("checked", true);
					self.label.addClass(aC);

					if(typeof(cB) === "function")
						cB(self.checkbox, self.label);
				} else {
					self.checkbox.prop("checked", false);
					self.label.removeClass(aC);

					if(typeof(cB) === "function")
						cB(self.checkbox, self.label);
				}
				evt.stopPropagation();
			});
			//cB(this.checkbox, this.label);
		}
		//setup
		this.each(function() {
			new $checkboxUI($(this));
		});
	}
});

jQuery.fn.extend({
	addRadioUI : function(cB, aC) {
		//radioUI Class

		$radioUI = function(jEl) {
			var self = this;
			this.radio = jEl;
			// el = jQuery Obj
			this.label = jEl.next();

			//pre-processor: check whether when page load,
			//               the checkbox is Checked or Not
			self.label.attr("name", self.radio.attr("name"));
			//console.log(self.radio.prop("checked"));
			if(self.radio.prop("checked") == true) {
				self.label.addClass(aC);
			}

			this.label.bind("click", function(evt) {
				if(jQuery(this).hasClass("RadioDisabled")) {
					return false;
				} else {
					$('input:radio[name="' + self.radio.attr('name') + '"]').each(function() {
						var label = $(this).parent().find("label[for='" + $(this).attr("id") + "']")
						if(label.hasClass(aC)) {
							label.removeClass(aC);
						}
					});
				}
				self.label.addClass(aC);
				evt.stopPropagation();
			});
			cB(this.radio, this.label);
		}
		//setup
		this.each(function() {
			new $radioUI($(this));
		});
	}
});

jQuery.fn.extend({
	addRadioUI2 : function(cB, aC) {
		//radioUI Class

		$radioUI = function(jEl) {
			var self = this;
			this.radio = jEl;
			// el = jQuery Obj
			this.label = jEl.next();

			//pre-processor: check whether when page load,
			//               the checkbox is Checked or Not
			self.label.attr("name", self.radio.attr("name"));
			if(self.radio.prop("checked") == true) {
				self.label.addClass(aC);
			}

			this.label.bind("click", function(evt) {
				if(jQuery(this).hasClass("RadioDisabled")) {
					return false;
				} else {
					$('input:radio[name="' + self.radio.attr('name') + '"]').each(function() {
						var label = $(this).parent().find("label[for='" + $(this).attr("id") + "']")
						if(label.hasClass(aC)) {
							label.removeClass(aC);
						}
					});
					jQuery(this).prev().prop("checked", "true");
					// Thao edit 11/05/2012 fix bug Box Survey
				}
				self.label.addClass(aC);
				evt.stopPropagation();
			});
			cB(this.radio, this.label);
		}
		//setup
		this.each(function() {
			new $radioUI($(this));
		});
	}
});
