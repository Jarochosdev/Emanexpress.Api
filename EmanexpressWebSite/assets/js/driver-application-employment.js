$( document ).ready(function() {
var base_color = "rgb(110,126,142)";
var active_color = "#FF2C1D";



var child = 1;
var length = $("section").length - 1;
$("#prev").addClass("disabled");
$("#submit").addClass("disabled");

$("section").not("section:nth-of-type(1)").hide();
$("section").not("section:nth-of-type(1)").css('transform','translateX(100px)');

var svgWidth = length * 200 + 24;
$("#svg_wrap").html(
  '<svg version="1.1" id="svg_form_time" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 ' +
    svgWidth +
    ' 24" xml:space="preserve"></svg>'
);

function makeSVG(tag, attrs) {
  var el = document.createElementNS("http://www.w3.org/2000/svg", tag);
  for (var k in attrs) el.setAttribute(k, attrs[k]);
  return el;
}

function getDate(elementId){			
	var date = document.getElementById(elementId).value
	if(date){
		return date;
	}
	
	return null;
}

function GetDifferenceInYears(difference){
	var seconds = difference/1000
	var minutes = seconds /60
	var hours = minutes/60
	var days = hours/24
	var years = days/365
	
	return years
}

for (i = 0; i < length; i++) {
  var positionX = 12 + i * 200;
  var rect = makeSVG("rect", { x: positionX, y: 9, width: 200, height: 6 });
  document.getElementById("svg_form_time").appendChild(rect);
  // <g><rect x="12" y="9" width="200" height="6"></rect></g>'
  var circle = makeSVG("circle", {
    cx: positionX,
    cy: 12,
    r: 12,
    width: positionX,
    height: 6
  });
  document.getElementById("svg_form_time").appendChild(circle);
}

var circle = makeSVG("circle", {
  cx: positionX + 200,
  cy: 12,
  r: 12,
  width: positionX,
  height: 6
});
document.getElementById("svg_form_time").appendChild(circle);

$('#svg_form_time rect').css('fill',base_color);
$('#svg_form_time circle').css('fill',base_color);
$("circle:nth-of-type(1)").css("fill", active_color);

 
$(".button").click(function () {  
  
	//alert(document.getElementsByTagName('section')[0].getElementById('first_name').value)

	var thereIsOneRequiredInputEmpty = false
	var actualSection = $("section:nth-of-type(" + child + ")");  
  
	actualSection.find('input').each(function() {        
		var id = $(this).attr("id")
		var name = $(this).attr("name")
		var element = undefined
		if(id === null || id === undefined || id.trim() === '')
		{
			element = document.getElementsByName(name)[0]
		}else{			
			element = document.getElementById(id)
		}
		
		if((element.type === 'text' || element.type === 'password') && element.required && (element.value === 'null' || element.value === 'undefined' || element.value.trim() === '')){			
			element.className = element.className + ' text-required';
			thereIsOneRequiredInputEmpty = true;
		}
		
		if(element.type === 'radio' && element.required){			//Verify if there is one checked			
			var radios = document.getElementsByName(element.name);			
			var thereIsOneRadioSelected = false;
			for (var i = 0, length = radios.length; i < length; i++) {
				if (radios[i].checked) {					
					thereIsOneRadioSelected = true;
					break;
				}
			}
			
			if(!thereIsOneRadioSelected){
				var radios = document.getElementsByName(element.name);
				for (var i = 0, length = radios.length; i < length; i++) {
					radios[i].className = element.className + ' radio-required';					
				}
				thereIsOneRequiredInputEmpty = true;
			}
		}
		
		if(element.type === 'checkbox' && element.required){
			var checkOptions = document.getElementsByName(element.name);
			var isCheckedSelected = true;
			for (var i = 0, length = checkOptions.length; i < length; i++) {
				if (!checkOptions[i].checked) {
					isCheckedSelected = false;
					break;
				}
			}
			
			if(!isCheckedSelected){
				var checkOptions = document.getElementsByName(element.name);
				for (var i = 0, length = checkOptions.length; i < length; i++) {
					checkOptions[i].className = element.className + ' check-required';					
				}
				thereIsOneRequiredInputEmpty = true;
			}
		}
	});
	
	if(child == 2)
	{
		var totalNumberOfYearsAddress = 0;
		
		for(i = 0; i < 4; i++) {
			if(document.getElementById("address_living_from_" + i).value && document.getElementById("address_living_to_" + i).value){				
				var livingFrom = Date.parse(getDate("address_living_from_" + i))
				var livingTo = Date.parse(getDate("address_living_to_" + i))
				var difference = livingTo - livingFrom;								
				totalNumberOfYearsAddress += GetDifferenceInYears(difference);
			}
		}
		
		if(totalNumberOfYearsAddress < 3){
			document.getElementById("address-3-value-message").innerHTML = "<strong>Error!</strong> You should specify the address for the last 3 years. Thanks.";
			
			$("#address-3-value-message").show();
			window.setTimeout(function() {
				$("#address-3-value-message").slideUp(1000, function(){});
			}, 5000);
			return;
		}
	}
	
	if(child == 3)
	{
		var totalNumberOfYearsEmployment = 0;
		
		for(i = 0; i < 4; i++) {
			if(document.getElementById("employment_from_" + i).value && document.getElementById("employment_to_" + i).value){				
				var employmentFrom = Date.parse(getDate("employment_from_" + i))
				var employmentTo = Date.parse(getDate("employment_to_" + i))
				var difference = employmentTo - employmentFrom;								
				totalNumberOfYearsEmployment += GetDifferenceInYears(difference);
			}
		}
		
		if(totalNumberOfYearsEmployment < 2){
			document.getElementById("address-3-value-message").innerHTML = "<strong>Error!</strong> You should specify the employment for the last 2 years. Thanks.";
			
			$("#address-3-value-message").show();
			window.setTimeout(function() {
				$("#address-3-value-message").slideUp(1000, function(){});
			}, 5000);
			return;
		}
	}
  
	if(!thereIsOneRequiredInputEmpty){  
		$("#svg_form_time rect").css("fill", active_color);
		$("#svg_form_time circle").css("fill", active_color);
		var id = $(this).attr("id");
		
		if (id == "next") {
			$("#prev").removeClass("disabled");
			if (child >= length) {
			$(this).addClass("disabled");
			$('#submit').removeClass("disabled");
			}
			if (child <= length) {
			child++;
			}
		} else if (id == "prev") {
			$("#next").removeClass("disabled");
			$('#submit').addClass("disabled");
			if (child <= 2) {
			$(this).addClass("disabled");
			}
			if (child > 1) {
			child--;
			}
		}
		
		var circle_child = child + 1;
		$("#svg_form_time rect:nth-of-type(n + " + child + ")").css(
		"fill",
		base_color
		);
		$("#svg_form_time circle:nth-of-type(n + " + circle_child + ")").css(
		"fill",
		base_color
		);
			
		var currentSection = $("section:nth-of-type(" + child + ")");   
		currentSection.fadeIn();
		currentSection.css('transform','translateX(0)');
		currentSection.prevAll('section').css('transform','translateX(-100px)');
		currentSection.nextAll('section').css('transform','translateX(100px)');
		
		if(child==10)
		{
			allowSubmit();
		}
		
		$('section').not(currentSection).hide();
	}else{
		$("#required-value-message").show();			
		window.setTimeout(function() {			
			$("#required-value-message").slideUp(500, function(){
				//$(this).style.opacity = '1';
			});
		}, 5000);
	}
});

});