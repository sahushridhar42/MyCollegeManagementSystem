$(document).ready(function() {
    $(".onlyNumberWithDecimal").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
             // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) || 
             // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
                 // let it happen, don't do anything
                 return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    
    $(".onlyNumberWithOutDecimal").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
             // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) || 
             // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
                 // let it happen, don't do anything
                 return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    
    $(".onlyAlphaNumeric").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
             // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) || 
             // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39) || (e.keyCode >= 65 && e.keyCode <= 90) || (e.keyCode >= 97 && e.keyCode <= 122)) {
                 // let it happen, don't do anything
                 return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    
    $(".mail").blur(function () {
        var emailTxt = $(".mail").val();
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;


        if (emailTxt == '') {
            //$("#txEmailId").after('<span class="error">Please enter your email address.</span>');
            $(".mail").next('.help-block').css('color', 'red').html('');
            $(".mail").next('.help-block').css('color', 'red').html('Please enter your email address.');
            hasError = true;
        }

        else if (!emailReg.test(emailTxt)) {
            //$("#txEmailId").after('<span class="error">Enter a valid email address.</span>');
            $(".mail").next('.help-block').css('color', 'red').html('');
            $(".mail").next('.help-block').css('color', 'red').html('Enter a valid email address.');
            hasError = true;
        }

        if (hasError == true) { return false; }

    });
    
    //$("#precieverEmailId").focusout(function () {
    //    var emailTxt = $("#precieverEmailId,precieverEmailId").val();
    //    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

    //    if (emailTxt.match(emailReg) || emailTxt == "")
    //        //if (emailTxt == '') {
    //        alert("Enter valid EmailID");
    //    //    //$("#txEmailId").after('<span class="error">Please enter your email address.</span>');
    //    //    $("#precieverEmailId").next('.help-block').css('color', 'red').html('');
    //    //    $("#precieverEmailId").next('.help-block').css('color', 'red').html('Please enter your email address.');
    //    //    hasError = true;
    //    //}

    //    //else if (!emailReg.test(emailTxt)) {
    //    //    //$("#txEmailId").after('<span class="error">Enter a valid email address.</span>');
    //    //    $("#precieverEmailId").next('.help-block').css('color', 'red').html('');
    //    //    $("#precieverEmailId").next('.help-block').css('color', 'red').html('Enter a valid email address.');
    //    //    hasError = true;
    //    //}

    //    //if (hasError == true) { return false; }

    //});
    //function validateEmail(email) {
    //    var re = /^[_a-z0-9-A-Z-]+(\.[_a-z0-9-A-Z-]+)*@[a-z0-9-A-Z-]+(\.[a-z0-9-A-Z-]+)*(\.[a-z]{2,4})$/;
    //    return re.test(email);
    //}
    //$("#recieverEmailId").focusout(function () {
    //    var emailTxt = $("#recieverEmailId").val();
    //    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

    //    if (emailTxt.match(emailReg) || emailTxt == "")
    //        //if (emailTxt == '') {
    //        alert("Enter valid EmailID");
    //    //    //$("#txEmailId").after('<span class="error">Please enter your email address.</span>');
    //    //    $("#precieverEmailId").next('.help-block').css('color', 'red').html('');
    //    //    $("#precieverEmailId").next('.help-block').css('color', 'red').html('Please enter your email address.');
    //    //    hasError = true;
    //    //}

    //    //else if (!emailReg.test(emailTxt)) {
    //    //    //$("#txEmailId").after('<span class="error">Enter a valid email address.</span>');
    //    //    $("#precieverEmailId").next('.help-block').css('color', 'red').html('');
    //    //    $("#precieverEmailId").next('.help-block').css('color', 'red').html('Enter a valid email address.');
    //    //    hasError = true;
    //    //}

    //    //if (hasError == true) { return false; }

    //});
    ////$("#recieverEmailId").blur(function () {
    //    var emailTxt = $("#recieverEmailId").val();
    //    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;


    //    if (emailTxt == '') {
    //        //$("#txEmailId").after('<span class="error">Please enter your email address.</span>');
    //        $("#recieverEmailId").next('.help-block').css('color', 'red').html('');
    //        $("#recieverEmailId").next('.help-block').css('color', 'red').html('Please enter your email address.');
    //        hasError = true;
    //    }

    //    else if (!emailReg.test(emailTxt)) {
    //        //$("#txEmailId").after('<span class="error">Enter a valid email address.</span>');
    //        $("#recieverEmailId").next('.help-block').css('color', 'red').html('');
    //        $("#recieverEmailId").next('.help-block').css('color', 'red').html('Enter a valid email address.');
    //        hasError = true;
    //    }

    //    if (hasError == true) { return false; }

    //});
    
    $("#allLetterToCaps").bind('keyup', function (e) {
        if (e.which >= 97 && e.which <= 122) {
            var newKey = e.which - 32;
            // I have tried setting those
            e.keyCode = newKey;
            e.charCode = newKey;
        }

        $("#textbox").val(($("#textbox").val()).toUpperCase());
    });
});


function dateAddDays( /*string dd/mm/yyyy*/ datstr, /*int*/ ndays){
	  var dattmp = datstr.split('/').reverse().join('/'),
	      nwdate =  new Date(dattmp);
	  nwdate.setDate(nwdate.getDate()+ndays||1);
	  return [ zeroPad(nwdate.getDate(),10)
	          ,zeroPad(nwdate.getMonth()+1,10)
	          ,nwdate.getFullYear() ].join('/');
	}

	//function to add zero to date/month < 10
	function zeroPad(nr,base){
	  var len = (String(base).length - String(nr).length)+1;
	  return len > 0? new Array(len).join('0')+nr : nr;
	}
	
	
	function changeToCurrency(x)
	{
		//var x=1400000;
		x=x.toString();
		var lastThree = x.substring(x.length-3);
		var otherNumbers = x.substring(0,x.length-3);
		if(otherNumbers != '')
		    lastThree = ',' + lastThree;
		var res = otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThree;
		return res;

	}