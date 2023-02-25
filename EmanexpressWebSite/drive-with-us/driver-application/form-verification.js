window.onload = function(){
	scrollToTop();
	setApplicationSendDate();								
	setApplicationForEmploymentData();
	displayLocalStorageAlert();
};

function setApplicationSendDate(){
	
	var dt = new Date();
	var currentDate = formatDate(dt)
	
	var element = document.getElementById("date_apply");
	element.innerHTML = currentDate
	
	var element = document.getElementById("date_certification");
	element.innerHTML = currentDate
}

function cancelApplication() {                
    if (confirm("Do you want to cancel the application?")) {
		localStorage.setItem('applicationFormEmployment', null);
		location.reload();
    }
}

function formatDate(date) {
	var d = new Date(date)
	var month = '' + (d.getMonth() + 1)
	var day = '' + d.getDate()
	var year = d.getFullYear()

	if (month.length < 2) 
		month = '0' + month;
	if (day.length < 2) 
		day = '0' + day;

	return [month, day, year].join('/');
}

function allowSubmit(){				
	var licenseNo = document.getElementById('driver_license_no_compliance')								
	var stateCompliance = document.getElementById('driver_license_state_compliance')								
	var expirationCompliance = document.getElementById('driver_license_expiration_compliance')								
	var certificationName = document.getElementById('driver_certification_name')								
	
	var agreeCheckBox = document.getElementsByName('driver_license_agree_compliance')[0]				
	var agreeIsChecked = agreeCheckBox.checked
	
	if(!agreeIsChecked || isEmpty(licenseNo) || isEmpty(stateCompliance) || isEmpty(expirationCompliance)|| isEmpty(certificationName))
	{								
		$("#submit").addClass("disabled");
	}else{
		$('#submit').removeClass("disabled");					
	}
}

function isEmpty(element){
	return element.value === 'null' || element.value === 'undefined' || element.value.trim() === ''
}

function scrollToTop() { 
	document.body.scrollTop = 0; // For Safari
	document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}

function agreeAndSendApplication(){				
	var xhr = new XMLHttpRequest();		
	xhr.open("POST", api_url + "DriverEmploymentApplications", true);
	xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
	
	xhr.onreadystatechange = function () {		
		if (xhr.readyState === 4) {	
			if (this.status == 200) {
				$("#sending_application").hide();
				$("#application_sent_success").show();						
				localStorage.setItem('applicationFormEmployment', null);
			}else{
				alert("Sorry! There was an error. Please Try again.")
				location.reload();
			}
		}
	};
					
		
	var jsonObject = JSON.stringify({
		"FirstName": document.getElementById("first_name").value, 
		"MiddleName": document.getElementById("middle_name").value, 
		"LastName": document.getElementById("last_name").value, 
		"Phone": document.getElementById("phone").value, 
		"DriverEmail": document.getElementById("email").value, 
		"DateOfBirth": getDate("date_of_birth"), 		
		"DateAvailableToStart": getDate("date_available_to_start"), 
		"PositionAppliedfor": document.getElementById("position_applied").value, 
		"HaveLegalRightToWorkInUsa": YesNoToBool(getSelectedValueFromRadio("legal_right")),
		"WorkedBeforeForUsWhere": document.getElementById("worked_with_us_where").value, 
		"WorkedBeforeForUsFrom": getDate("worked_with_us_from"), 
		"WorkedBeforeForUsTo": getDate("worked_with_us_to"), 
		"WorkedBeforeForUsRateOfPay": document.getElementById("worked_with_us_rate_of_pay").value, 
		"WorkedBeforeForUsPosition": document.getElementById("worked_with_us_position").value, 
		"WorkedBeforeForUsReasonOfLeaving": document.getElementById("worked_with_us_reason_of_leaving").value, 
		"AreYouNowEmployed": YesNoToBool(getSelectedValueFromRadio("now_employed")),
		"HowLongSinceLastEmployment": document.getElementById("time_since_last_employment").value, 
		"WhoReferedYou": document.getElementById("who_refered_you").value, 
		"RateOfPayExpected": document.getElementById("rate_of_pay_expected").value, 
		"HaveYouEverBeenBonded": YesNoToBool(getSelectedValueFromRadio("bonded")),
		"NameOfBondingCompany": document.getElementById("bonding_company").value, 
		"HaveYouEverBeenConvictedOfAFelony": YesNoToBool(getSelectedValueFromRadio("convicted_felony")),
		"FelonyDetails": document.getElementById("felony_details").value, 
		"UnableToPerformFunctionsJob": YesNoToBool(getSelectedValueFromRadio("unable_to_perform_job")),
		"UnableToPerformFunctionsJobDetails": document.getElementById("unable_to_perform_job_details").value,
		"AddressHistory" : [ getAddressServerRequest(0), getAddressServerRequest(1), getAddressServerRequest(2), getAddressServerRequest(3)],
		"EmploymentHistory" : [ getEmploymentHistoryServerRequest(0), getEmploymentHistoryServerRequest(1), getEmploymentHistoryServerRequest(2), getEmploymentHistoryServerRequest(3)],
		"AccidentRecords" : [ getAccidentRecordServerRequest(0), getAccidentRecordServerRequest(1), getAccidentRecordServerRequest(2), getAccidentRecordServerRequest(3)],
		'TrafficConvictions': [getTrafficConvictionServerRequest(0), getTrafficConvictionServerRequest(1), getTrafficConvictionServerRequest(2), getTrafficConvictionServerRequest(3)],
		'LicenseHistory': [getLicenseHistoryServerRequest(0), getLicenseHistoryServerRequest(1), getLicenseHistoryServerRequest(2), getLicenseHistoryServerRequest(3), getLicenseHistoryServerRequest(4)],
		'HaveYouBeenDeniedALicense': YesNoToBool(getSelectedValueFromRadio("denied_motor_license")),
		'HasAnyLicenseBeenSuspended': YesNoToBool(getSelectedValueFromRadio("suspended_license")),
		'LicenseSuspendedDetail': document.getElementById("denied_suspended_details").value,					
		'DrivingExperience': 
			[getDrivingExperienceEquipmentServerRequest(0, 'Straight Truck'), 
			getDrivingExperienceEquipmentServerRequest(1, 'Tractor and Semi-Trailer'), 
			getDrivingExperienceEquipmentServerRequest(2, 'Tractor-Two Trailers'), 
			getDrivingExperienceEquipmentServerRequest(3, 'Tractor-Three Trailers'), 
			getDrivingExperienceEquipmentServerRequest(4, 'Motorcoach-SB (+8 pax)'), 
			getDrivingExperienceEquipmentServerRequest(5, 'Motorcoach-SB (+15 pax)'),
			getDrivingExperienceEquipmentOtherServerRequest()],
		'StatesOperatedForLastYears': document.getElementById("driving_experience_states_operated").value,
		'SpecialCoursesOfTraining': document.getElementById("driving_experience_special_courses").value,
		'SafeDrivingAwards': document.getElementById("driving_experience_driving_awards").value,
		'OtherExperience': document.getElementById("other_experience").value,
		'CoursesAndTraining': document.getElementById("course_and_training").value,
		'SpecialEquipment': document.getElementById("special_equipments").value,
		'HighestGradeCompleted': document.getElementById("highest_grade_completed").value,
		'HighSchool': document.getElementById("highest_high_school_completed").value,
		'CollegeLevel': document.getElementById("highest_college_completed").value,					
		'LastAttendedSchool': document.getElementById("last_school_attended").value,	
		'EnrolledInClearingHouse': YesNoToBool(getSelectedValueFromRadio("clearing_house")),
		'DriverNameSignature': document.getElementById("driver_name_signature").value,
		'DriverApplicationAgree': getBoolCheck("agree_application"),
		'ApplicationDate': new Date(),
		'LicenseCertificationOfCompliance': {
			'LicenseNumber': document.getElementById("driver_license_no_compliance").value,
			'LicenseState': document.getElementById("driver_license_state_compliance").value,
			'LicenseExpiration': getDate("driver_license_expiration_compliance"),
			'DriverNamePrinted': document.getElementById("driver_certification_name").value,
			'CertificationAgree': getBoolCheck("driver_license_agree_compliance")
		}
	});
	
	$("#submit").hide();				
	$("#last_step").hide();
	$("#cancel_application").hide();				
	$("#sending_application").show();	
	$("#prev").hide();				
	
	xhr.send(jsonObject);
}

function getBoolCheck(name){
	return document.getElementsByName(name)[0].checked;				
}

function getDate(elementId){			
	var date = document.getElementById(elementId).value
	if(date){
		return date;
	}
	
	return null;
}

function getAddressServerRequest(position){
	var address = { };				
	address['Street'] = document.getElementById("address_street_" + position).value;
	address['City'] =document.getElementById("address_city_" + position).value;
	address['State'] = document.getElementById("address_state_" + position).value;
	address['ZipCode'] = document.getElementById("address_zip_code_" + position).value;	
	address['LivingFrom'] = getDate("address_living_from_" + position);
	address['LivingTo'] = getDate("address_living_to_" + position);
	address['StillLeavingHere'] = (position == 0) ? true : false;
	return address
}

function getEmploymentHistoryServerRequest(position){
	var employment_history = { };
	employment_history['CompanyName'] = document.getElementById("employment_name_" + position).value;
	employment_history['Address'] =document.getElementById("employment_address_" + position).value;
	employment_history['City'] = document.getElementById("employment_city_" + position).value;
	employment_history['State'] = document.getElementById("employment_state_" + position).value;
	employment_history['Zipcode'] = document.getElementById("employment_zip_code_" + position).value;
	employment_history['ContactPerson'] = document.getElementById("employment_contact_" + position).value;
	employment_history['PhoneNumber'] = document.getElementById("employment_phone_" + position).value;
	employment_history['From'] = getDate("employment_from_" + position);
	employment_history['To'] = getDate("employment_to_" + position);
	employment_history['Salary'] = document.getElementById("employment_salary_" + position).value;
	employment_history['PositionHeld'] = document.getElementById("employment_position_" + position).value;
	employment_history['ReasonForLeaving'] = document.getElementById("employment_reason_for_leaving_" + position).value;
	employment_history['SubjectToMfcsrs'] = YesNoToBool(getSelectedValueFromRadio('FMCSRs_' + position));
	employment_history['SafetySensitive'] = YesNoToBool(getSelectedValueFromRadio('safety_sensitive_' + position));
	employment_history['StillWorkingHere'] = (position == 0) ? true : false;
	return employment_history
}

function getAccidentRecordServerRequest(position){
	var accident_record = { };				
	accident_record['AccidentDate'] = getDate("accident_record_date_" + position);
	accident_record['NatureOfAccident'] =document.getElementById("accident_record_nature_" + position).value;
	accident_record['Fatalities'] = document.getElementById("accident_record_Fatalities_" + position).value;
	accident_record['Injuries'] = document.getElementById("accident_record_Injuries_" + position).value;
	accident_record['HazardousMaterial'] = document.getElementById("accident_record_hazardous_material_" + position).value
	return accident_record
}
			
function getTrafficConvictionServerRequest(position){
	var traffic_conviction = { };				
	traffic_conviction['Date'] = getDate("traffic_conviction_date_" + position);
	traffic_conviction['Location'] =document.getElementById("traffic_conviction_location_" + position).value;
	traffic_conviction['Charge'] = document.getElementById("traffic_conviction_charge_" + position).value;
	traffic_conviction['Penalty'] = document.getElementById("traffic_conviction_penalty_" + position).value;				
	return traffic_conviction
}

function getLicenseHistoryServerRequest(position){
	var license_history = { };				
	license_history['State'] = document.getElementById("license_history_state_" + position).value;
	license_history['LicenseNumber'] =document.getElementById("license_history_license_no_" + position).value;
	license_history['Class'] = document.getElementById("license_history_class_" + position).value;
	license_history['Endorsement'] = document.getElementById("license_history_endorsement_" + position).value;
	license_history['Expiration'] = getDate("license_history_expiration_" + position);				
	return license_history
}
			
function getDrivingExperienceEquipmentServerRequest(position, name){
	var driving_experience_equipment = { };				
	driving_experience_equipment['Name'] = name;
	driving_experience_equipment['TypeOfEquipment'] = document.getElementById("driving_experience_type_" + position).value;
	driving_experience_equipment['FromMonthYear'] =document.getElementById("driving_experience_from_" + position).value;
	driving_experience_equipment['ToMonthYear'] = document.getElementById("driving_experience_to_" + position).value;
	driving_experience_equipment['AproxMiles'] = document.getElementById("driving_experience_miles_" + position).value;
	return driving_experience_equipment
}

function getDrivingExperienceEquipmentOtherServerRequest(){
	var driving_experience_equipment = { };				
	driving_experience_equipment['Name'] = document.getElementById("driving_experience_other_name").value;
	driving_experience_equipment['TypeOfEquipment'] = document.getElementById("driving_experience_other_type").value;
	driving_experience_equipment['FromMonthYear'] =document.getElementById("driving_experience_other_from").value;
	driving_experience_equipment['ToMonthYear'] = document.getElementById("driving_experience_other_to").value;
	driving_experience_equipment['AproxMiles'] = document.getElementById("driving_experience_other_miles").value;				
	return driving_experience_equipment
}

function YesNoToBool(value){

	if(value === 'YES'){
		return true;
	}
	
	return false;
}

function displayLocalStorageAlert(){
	if(localStorage){				
		$("#local_saving_supported").show();
	}else{
		$("#local_saving_supported").show();
	}
}

function setApplicationForEmploymentData(){
	if(localStorage) {
		var applicationFormEmployment = localStorage.getItem('applicationFormEmployment');
		if(applicationFormEmployment && applicationFormEmployment !== 'null' && applicationFormEmployment !== 'undefined'){						
			setInputValues(JSON.parse(applicationFormEmployment))
		}
	}
}

function saveDataLocally() {
	if(localStorage) {
		var stringValues = JSON.stringify(getInputValues())					
		localStorage.setItem('applicationFormEmployment', stringValues);
	}
}
				  			
function getInputValues(){
	var applicationFormEmployment = { 
		'first_name': document.getElementById("first_name").value, 
		'last_name': document.getElementById("last_name").value,
		'middle_name': document.getElementById("middle_name").value,
		'phone': document.getElementById("phone").value,
		'email': document.getElementById("email").value,
		'date_of_birth': document.getElementById("date_of_birth").value,
		'date_available_to_start': document.getElementById("date_available_to_start").value,
		'position_applied': document.getElementById("position_applied").value,
		'legal_right': getSelectedValueFromRadio("legal_right"),
		'worked_with_us_where': document.getElementById("worked_with_us_where").value,
		'worked_with_us_from': document.getElementById("worked_with_us_from").value,
		'worked_with_us_to': document.getElementById("worked_with_us_to").value,
		'worked_with_us_rate_of_pay': document.getElementById("worked_with_us_rate_of_pay").value,
		'worked_with_us_position': document.getElementById("worked_with_us_position").value,
		'worked_with_us_reason_of_leaving': document.getElementById("worked_with_us_reason_of_leaving").value,
		'now_employed': getSelectedValueFromRadio("now_employed"),
		'time_since_last_employment': document.getElementById("time_since_last_employment").value,
		'who_refered_you': document.getElementById("who_refered_you").value,
		'rate_of_pay_expected': document.getElementById("rate_of_pay_expected").value,
		'bonded': getSelectedValueFromRadio("bonded"),					
		'bonding_company': document.getElementById("bonding_company").value,
		'convicted_felony': getSelectedValueFromRadio("convicted_felony"),					
		'felony_details': document.getElementById("felony_details").value,
		'unable_to_perform_job': getSelectedValueFromRadio("unable_to_perform_job"),
		'unable_to_perform_job_details': document.getElementById("unable_to_perform_job_details").value,
		'address': [ getAddress(0), getAddress(1), getAddress(2), getAddress(3)],
		'employment_history': [getEmploymentHistory(0), getEmploymentHistory(1), getEmploymentHistory(2), getEmploymentHistory(3)],
		'accident_record': [getAccidentRecord(0), getAccidentRecord(1), getAccidentRecord(2), getAccidentRecord(3)],
		'traffic_conviction': [getTrafficConviction(0), getTrafficConviction(1), getTrafficConviction(2), getTrafficConviction(3)],
		'license_history': [getLicenseHistory(0), getLicenseHistory(1), getLicenseHistory(2), getLicenseHistory(3), getLicenseHistory(4)],
		'denied_motor_license': getSelectedValueFromRadio("denied_motor_license"),
		'suspended_license': getSelectedValueFromRadio("suspended_license"),
		'denied_suspended_details': document.getElementById("denied_suspended_details").value,
		'driving_experience_equipment': [getDrivingExperienceEquipment(0), getDrivingExperienceEquipment(1), getDrivingExperienceEquipment(2), getDrivingExperienceEquipment(3), getDrivingExperienceEquipment(4), getDrivingExperienceEquipment(5)],
		'driving_experience_equipment_other': getDrivingExperienceEquipmentOther(),
		'driving_experience_states_operated': document.getElementById("driving_experience_states_operated").value,
		'driving_experience_special_courses': document.getElementById("driving_experience_special_courses").value,
		'driving_experience_driving_awards': document.getElementById("driving_experience_driving_awards").value,
		'other_experience': document.getElementById("other_experience").value,
		'course_and_training': document.getElementById("course_and_training").value,
		'special_equipments': document.getElementById("special_equipments").value,
		'highest_grade_completed': document.getElementById("highest_grade_completed").value,
		'highest_high_school_completed': document.getElementById("highest_high_school_completed").value,
		'highest_college_completed': document.getElementById("highest_college_completed").value,					
		'last_school_attended': document.getElementById("last_school_attended").value,
		'clearing_house': getSelectedValueFromRadio("clearing_house"),
		'driver_license_no_compliance': document.getElementById("driver_license_no_compliance").value,
		'driver_license_state_compliance': document.getElementById("driver_license_state_compliance").value,
		'driver_license_expiration_compliance': document.getElementById("driver_license_expiration_compliance").value			
	};
					
	return applicationFormEmployment
}							
				
function getAddress(position){
	var address = { };				
	address['address_street_' + position] = document.getElementById("address_street_" + position).value;
	address['address_city_' + position] =document.getElementById("address_city_" + position).value;
	address['address_state_' + position] = document.getElementById("address_state_" + position).value;
	address['address_zip_code_' + position] = document.getElementById("address_zip_code_" + position).value;
	address['address_living_from_' + position] = getDate("address_living_from_" + position);			
	address['address_living_to_' + position] = getDate("address_living_to_" + position);
	return address
}

function getAccidentRecord(position){
	var accident_record = { };				
	accident_record['accident_record_date_' + position] = document.getElementById("accident_record_date_" + position).value;
	accident_record['accident_record_nature_' + position] =document.getElementById("accident_record_nature_" + position).value;
	accident_record['accident_record_Fatalities_' + position] = document.getElementById("accident_record_Fatalities_" + position).value;
	accident_record['accident_record_Injuries_' + position] = document.getElementById("accident_record_Injuries_" + position).value;
	accident_record['accident_record_hazardous_material_' + position] = document.getElementById("accident_record_hazardous_material_" + position).value;				
	return accident_record
}

function getEmploymentHistory(position){
	var employment_history = { };				
	employment_history['employment_name_' + position] = document.getElementById("employment_name_" + position).value;
	employment_history['employment_address_' + position] =document.getElementById("employment_address_" + position).value;
	employment_history['employment_city_' + position] = document.getElementById("employment_city_" + position).value;
	employment_history['employment_state_' + position] = document.getElementById("employment_state_" + position).value;
	employment_history['employment_zip_code_' + position] = document.getElementById("employment_zip_code_" + position).value;
	employment_history['employment_contact_' + position] = document.getElementById("employment_contact_" + position).value;
	employment_history['employment_phone_' + position] = document.getElementById("employment_phone_" + position).value;
	employment_history['employment_from_' + position] = document.getElementById("employment_from_" + position).value;
	employment_history['employment_to_' + position] = document.getElementById("employment_to_" + position).value;
	employment_history['employment_salary_' + position] = document.getElementById("employment_salary_" + position).value;
	employment_history['employment_position_' + position] = document.getElementById("employment_position_" + position).value;
	employment_history['employment_reason_for_leaving_' + position] = document.getElementById("employment_reason_for_leaving_" + position).value;
	employment_history['FMCSRs_' + position] = getSelectedValueFromRadio('FMCSRs_' + position);					
	employment_history['safety_sensitive_' + position] = getSelectedValueFromRadio('safety_sensitive_' + position);				
	return employment_history
}

function getTrafficConviction(position){
	var traffic_conviction = { };				
	traffic_conviction['traffic_conviction_date_' + position] = document.getElementById("traffic_conviction_date_" + position).value;
	traffic_conviction['traffic_conviction_location_' + position] =document.getElementById("traffic_conviction_location_" + position).value;
	traffic_conviction['traffic_conviction_charge_' + position] = document.getElementById("traffic_conviction_charge_" + position).value;
	traffic_conviction['traffic_conviction_penalty_' + position] = document.getElementById("traffic_conviction_penalty_" + position).value;				
	return traffic_conviction
}

function getLicenseHistory(position){
	var license_history = { };				
	license_history['license_history_state_' + position] = document.getElementById("license_history_state_" + position).value;
	license_history['license_history_license_no_' + position] =document.getElementById("license_history_license_no_" + position).value;
	license_history['license_history_class_' + position] = document.getElementById("license_history_class_" + position).value;
	license_history['license_history_endorsement_' + position] = document.getElementById("license_history_endorsement_" + position).value;
	license_history['license_history_expiration_' + position] = document.getElementById("license_history_expiration_" + position).value;
	return license_history
}

function getDrivingExperienceEquipment(position){
	var driving_experience_equipment = { };				
	driving_experience_equipment['driving_experience_is_checked_' + position] = document.getElementById("driving_experience_is_checked_" + position).checked;
	driving_experience_equipment['driving_experience_type_' + position] = document.getElementById("driving_experience_type_" + position).value;
	driving_experience_equipment['driving_experience_from_' + position] =document.getElementById("driving_experience_from_" + position).value;
	driving_experience_equipment['driving_experience_to_' + position] = document.getElementById("driving_experience_to_" + position).value;
	driving_experience_equipment['driving_experience_miles_' + position] = document.getElementById("driving_experience_miles_" + position).value;				
	return driving_experience_equipment
}

function getDrivingExperienceEquipmentOther(){
	var driving_experience_equipment = { };				
	driving_experience_equipment['driving_experience_other_name'] = document.getElementById("driving_experience_other_name").value;
	driving_experience_equipment['driving_experience_other_type'] = document.getElementById("driving_experience_other_type").value;
	driving_experience_equipment['driving_experience_other_from'] =document.getElementById("driving_experience_other_from").value;
	driving_experience_equipment['driving_experience_other_to'] = document.getElementById("driving_experience_other_to").value;
	driving_experience_equipment['driving_experience_other_miles'] = document.getElementById("driving_experience_other_miles").value;				
	return driving_experience_equipment
}
					
function setInputValues(jsonApplicationFormEmployment){
	document.getElementById("first_name").value = getValue(jsonApplicationFormEmployment.first_name);
	document.getElementById("last_name").value = getValue(jsonApplicationFormEmployment.last_name);
	document.getElementById("middle_name").value = getValue(jsonApplicationFormEmployment.middle_name);
	document.getElementById("phone").value = getValue(jsonApplicationFormEmployment.phone);
	document.getElementById("email").value = getValue(jsonApplicationFormEmployment.email);
	document.getElementById("date_of_birth").value = getValue(jsonApplicationFormEmployment.date_of_birth);
	document.getElementById("date_available_to_start").value = getValue(jsonApplicationFormEmployment.date_available_to_start);
	document.getElementById("position_applied").value = getValue(jsonApplicationFormEmployment.position_applied);				
	setRadioButtonByNameValue("legal_right", getValue(jsonApplicationFormEmployment.legal_right))				
	document.getElementById("worked_with_us_where").value = getValue(jsonApplicationFormEmployment.worked_with_us_where);
	document.getElementById("worked_with_us_from").value = getValue(jsonApplicationFormEmployment.worked_with_us_from);			
	document.getElementById("worked_with_us_to").value = getValue(jsonApplicationFormEmployment.worked_with_us_to);
	document.getElementById("worked_with_us_rate_of_pay").value = getValue(jsonApplicationFormEmployment.worked_with_us_rate_of_pay);
	document.getElementById("worked_with_us_position").value = getValue(jsonApplicationFormEmployment.worked_with_us_position);
	document.getElementById("worked_with_us_reason_of_leaving").value = getValue(jsonApplicationFormEmployment.worked_with_us_reason_of_leaving);
	setRadioButtonByNameValue("now_employed", getValue(jsonApplicationFormEmployment.now_employed));
	document.getElementById("time_since_last_employment").value = getValue(jsonApplicationFormEmployment.time_since_last_employment);			
	document.getElementById("who_refered_you").value = getValue(jsonApplicationFormEmployment.who_refered_you);				
	document.getElementById("rate_of_pay_expected").value = getValue(jsonApplicationFormEmployment.rate_of_pay_expected);
	setRadioButtonByNameValue("bonded", getValue(jsonApplicationFormEmployment.bonded));
	document.getElementById("bonding_company").value = getValue(jsonApplicationFormEmployment.bonding_company);
	setRadioButtonByNameValue("convicted_felony", getValue(jsonApplicationFormEmployment.convicted_felony));
	document.getElementById("felony_details").value = getValue(jsonApplicationFormEmployment.felony_details);
	setRadioButtonByNameValue("unable_to_perform_job", getValue(jsonApplicationFormEmployment.unable_to_perform_job));
	document.getElementById("unable_to_perform_job_details").value = getValue(jsonApplicationFormEmployment.unable_to_perform_job_details);
	
	if(jsonApplicationFormEmployment.address){
		setAddress(0, jsonApplicationFormEmployment.address[0]);
		setAddress(1, jsonApplicationFormEmployment.address[1]);
		setAddress(2, jsonApplicationFormEmployment.address[2]);
		setAddress(3, jsonApplicationFormEmployment.address[3]);
	}
	
	if(jsonApplicationFormEmployment.employment_history){
		setEmploymentHistory(0, jsonApplicationFormEmployment.employment_history[0]);
		setEmploymentHistory(1, jsonApplicationFormEmployment.employment_history[1]);
		setEmploymentHistory(2, jsonApplicationFormEmployment.employment_history[2]);
		setEmploymentHistory(3, jsonApplicationFormEmployment.employment_history[3]);
	}
	
	if(jsonApplicationFormEmployment.accident_record){
		setAccidentRecord(0, jsonApplicationFormEmployment.accident_record[0]);
		setAccidentRecord(1, jsonApplicationFormEmployment.accident_record[1]);
		setAccidentRecord(2, jsonApplicationFormEmployment.accident_record[2]);
		setAccidentRecord(3, jsonApplicationFormEmployment.accident_record[3]);
	}
	
	if(jsonApplicationFormEmployment.traffic_conviction){
		setTrafficConviction(0, jsonApplicationFormEmployment.traffic_conviction[0]);
		setTrafficConviction(1, jsonApplicationFormEmployment.traffic_conviction[1]);
		setTrafficConviction(2, jsonApplicationFormEmployment.traffic_conviction[2]);
		setTrafficConviction(3, jsonApplicationFormEmployment.traffic_conviction[3]);
	}
	
	if(jsonApplicationFormEmployment.license_history){
		setLicenseHistory(0, jsonApplicationFormEmployment.license_history[0]);
		setLicenseHistory(1, jsonApplicationFormEmployment.license_history[1]);
		setLicenseHistory(2, jsonApplicationFormEmployment.license_history[2]);
		setLicenseHistory(3, jsonApplicationFormEmployment.license_history[3]);
		setLicenseHistory(4, jsonApplicationFormEmployment.license_history[4]);
	}
	
	setRadioButtonByNameValue("denied_motor_license", getValue(jsonApplicationFormEmployment.denied_motor_license));
	setRadioButtonByNameValue("suspended_license", getValue(jsonApplicationFormEmployment.suspended_license));		
	document.getElementById("denied_suspended_details").value = getValue(jsonApplicationFormEmployment.denied_suspended_details);

	if(jsonApplicationFormEmployment.driving_experience_equipment){
		setDrivingExperienceEquipment(0, jsonApplicationFormEmployment.driving_experience_equipment[0]);
		setDrivingExperienceEquipment(1, jsonApplicationFormEmployment.driving_experience_equipment[1]);
		setDrivingExperienceEquipment(2, jsonApplicationFormEmployment.driving_experience_equipment[2]);
		setDrivingExperienceEquipment(3, jsonApplicationFormEmployment.driving_experience_equipment[3]);
		setDrivingExperienceEquipment(4, jsonApplicationFormEmployment.driving_experience_equipment[4]);
		setDrivingExperienceEquipment(5, jsonApplicationFormEmployment.driving_experience_equipment[5]);
	}
	
	if(jsonApplicationFormEmployment.driving_experience_equipment_other){
		setDrivingExperienceEquipmentOther(jsonApplicationFormEmployment.driving_experience_equipment_other)
	}
	
	document.getElementById("driving_experience_states_operated").value = getValue(jsonApplicationFormEmployment.driving_experience_states_operated);
	document.getElementById("driving_experience_special_courses").value = getValue(jsonApplicationFormEmployment.driving_experience_special_courses);
	document.getElementById("driving_experience_driving_awards").value = getValue(jsonApplicationFormEmployment.driving_experience_driving_awards);
	document.getElementById("other_experience").value = getValue(jsonApplicationFormEmployment.other_experience);
	document.getElementById("course_and_training").value = getValue(jsonApplicationFormEmployment.course_and_training);
	document.getElementById("special_equipments").value = getValue(jsonApplicationFormEmployment.special_equipments);
	
	document.getElementById("highest_grade_completed").value = getSelectedValue(jsonApplicationFormEmployment.highest_grade_completed);
	document.getElementById("highest_high_school_completed").value = getSelectedValue(jsonApplicationFormEmployment.highest_high_school_completed);
	document.getElementById("highest_college_completed").value = getSelectedValue(jsonApplicationFormEmployment.highest_college_completed);
	
	document.getElementById("last_school_attended").value = getValue(jsonApplicationFormEmployment.last_school_attended);
		
	setRadioButtonByNameValue("clearing_house", getValue(jsonApplicationFormEmployment.clearing_house));
	
	document.getElementById("driver_license_no_compliance").value = getValue(jsonApplicationFormEmployment.driver_license_no_compliance);
	document.getElementById("driver_license_state_compliance").value = getValue(jsonApplicationFormEmployment.driver_license_state_compliance);
	document.getElementById("driver_license_expiration_compliance").value = getValue(jsonApplicationFormEmployment.driver_license_expiration_compliance);					
}

function setAddress(position, jsonAddress){
	document.getElementById("address_street_" + position).value = getValue(jsonAddress["address_street_" + position]);
	document.getElementById("address_city_" + position).value = getValue(jsonAddress["address_city_" + position]);
	document.getElementById("address_state_" + position).value = getValue(jsonAddress["address_state_" + position]);
	document.getElementById("address_zip_code_" + position).value = getValue(jsonAddress["address_zip_code_" + position]);
	document.getElementById("address_living_from_" + position).value = getValue(jsonAddress["address_living_from_" + position]);
	document.getElementById("address_living_to_" + position).value = getValue(jsonAddress["address_living_to_" + position]);
}

function setEmploymentHistory(position, jsonEmploymentHistory){
	document.getElementById("employment_name_" + position).value = getValue(jsonEmploymentHistory["employment_name_" + position]);
	document.getElementById("employment_address_" + position).value = getValue(jsonEmploymentHistory["employment_address_" + position]);
	document.getElementById("employment_city_" + position).value = getValue(jsonEmploymentHistory["employment_city_" + position]);
	document.getElementById("employment_state_" + position).value = getValue(jsonEmploymentHistory["employment_state_" + position]);
	document.getElementById("employment_zip_code_" + position).value = getValue(jsonEmploymentHistory["employment_zip_code_" + position]);
	document.getElementById("employment_contact_" + position).value = getValue(jsonEmploymentHistory["employment_contact_" + position]);
	document.getElementById("employment_phone_" + position).value = getValue(jsonEmploymentHistory["employment_phone_" + position]);
	document.getElementById("employment_to_" + position).value = getValue(jsonEmploymentHistory["employment_to_" + position]);
	document.getElementById("employment_from_" + position).value = getValue(jsonEmploymentHistory["employment_from_" + position]);
	document.getElementById("employment_salary_" + position).value = getValue(jsonEmploymentHistory["employment_salary_" + position]);
	document.getElementById("employment_position_" + position).value = getValue(jsonEmploymentHistory["employment_position_" + position]);
	document.getElementById("employment_reason_for_leaving_" + position).value = getValue(jsonEmploymentHistory["employment_reason_for_leaving_" + position]);								
	setRadioButtonByNameValue("FMCSRs_" + position, getValue(jsonEmploymentHistory["FMCSRs_" + position]));
	setRadioButtonByNameValue("safety_sensitive_" + position, getValue(jsonEmploymentHistory["safety_sensitive_" + position]));				
}

function setAccidentRecord(position, jsonAccidentRecord){
	document.getElementById("accident_record_date_" + position).value = getValue(jsonAccidentRecord["accident_record_date_" + position]);
	document.getElementById("accident_record_nature_" + position).value = getValue(jsonAccidentRecord["accident_record_nature_" + position]);
	document.getElementById("accident_record_Fatalities_" + position).value = getValue(jsonAccidentRecord["accident_record_Fatalities_" + position]);
	document.getElementById("accident_record_Injuries_" + position).value = getValue(jsonAccidentRecord["accident_record_Injuries_" + position]);
	document.getElementById("accident_record_hazardous_material_" + position).value = getValue(jsonAccidentRecord["accident_record_hazardous_material_" + position]);
}

function setTrafficConviction(position, jsonTrafficConviction){
	document.getElementById("traffic_conviction_date_" + position).value = getValue(jsonTrafficConviction["traffic_conviction_date_" + position]);
	document.getElementById("traffic_conviction_location_" + position).value = getValue(jsonTrafficConviction["traffic_conviction_location_" + position]);
	document.getElementById("traffic_conviction_charge_" + position).value = getValue(jsonTrafficConviction["traffic_conviction_charge_" + position]);
	document.getElementById("traffic_conviction_penalty_" + position).value = getValue(jsonTrafficConviction["traffic_conviction_penalty_" + position]);				
}

function setLicenseHistory(position, jsonLicenseHistory){
	document.getElementById("license_history_state_" + position).value = getValue(jsonLicenseHistory["license_history_state_" + position]);
	document.getElementById("license_history_license_no_" + position).value = getValue(jsonLicenseHistory["license_history_license_no_" + position]);
	document.getElementById("license_history_class_" + position).value = getValue(jsonLicenseHistory["license_history_class_" + position]);
	document.getElementById("license_history_endorsement_" + position).value = getValue(jsonLicenseHistory["license_history_endorsement_" + position]);	
	document.getElementById("license_history_expiration_" + position).value = getValue(jsonLicenseHistory["license_history_expiration_" + position]);
}

function setDrivingExperienceEquipment(position, jsonDrivingExperience){
	document.getElementById("driving_experience_is_checked_" + position).checked = getCheckValue(jsonDrivingExperience["driving_experience_is_checked_" + position]);
	document.getElementById("driving_experience_type_" + position).value = getValue(jsonDrivingExperience["driving_experience_type_" + position]);
	document.getElementById("driving_experience_from_" + position).value = getValue(jsonDrivingExperience["driving_experience_from_" + position]);
	document.getElementById("driving_experience_to_" + position).value = getValue(jsonDrivingExperience["driving_experience_to_" + position]);
	document.getElementById("driving_experience_miles_" + position).value = getValue(jsonDrivingExperience["driving_experience_miles_" + position]);									
	setDisableDrivingExperienceControls(!getCheckValue(jsonDrivingExperience["driving_experience_is_checked_" + position]), position);
}
			

function setDrivingExperienceEquipmentOther(jsonDrivingExperienceOther){
	document.getElementById("driving_experience_other_name").value = getValue(jsonDrivingExperienceOther["driving_experience_other_name"]);
	document.getElementById("driving_experience_other_type").value = getValue(jsonDrivingExperienceOther["driving_experience_other_type"]);
	document.getElementById("driving_experience_other_from").value = getValue(jsonDrivingExperienceOther["driving_experience_other_from"]);
	document.getElementById("driving_experience_other_to").value = getValue(jsonDrivingExperienceOther["driving_experience_other_to"]);
	document.getElementById("driving_experience_other_miles").value = getValue(jsonDrivingExperienceOther["driving_experience_other_miles"]);
}
			
function toggleDrivingExperience(element, position){				
	if(element.checked){
		setDisableDrivingExperienceControls(false, position);
	}else{
		setDisableDrivingExperienceControls(true, position);					
		
		document.getElementById('driving_experience_type_' + position).value = '';
		document.getElementById('driving_experience_from_' + position).value = '';
		document.getElementById('driving_experience_to_' + position).value = '';
		document.getElementById('driving_experience_miles_' + position).value = '';
	}
	
	saveDataLocally();
}

function setDisableDrivingExperienceControls(value, position){
	document.getElementById('driving_experience_type_' + position).disabled = value;
	document.getElementById('driving_experience_from_' + position).disabled = value;
	document.getElementById('driving_experience_to_' + position).disabled = value;
	document.getElementById('driving_experience_miles_' + position).disabled = value;					
}

function getSelectedValueFromRadio(radioButtonName){
	var radios = document.getElementsByName(radioButtonName);
	var valueSelected = null;
	for (var i = 0, length = radios.length; i < length; i++) {
	  if (radios[i].checked) {					
		valueSelected = radios[i].value;
		break;
	  }
	}
	
	return valueSelected
}

function setRadioButtonByNameValue(radioButtonName, value){
	var radios = document.getElementsByName(radioButtonName);				
	for (var i = 0, length = radios.length; i < length; i++) {
	  if (radios[i].value === value) {					
		radios[i].checked = true;
		break;
	  }
	}				
}

function getValue(value){
	if(value){
		return value
	}			
	return ""
}

function getCheckValue(value){
	if(value){
		return value
	}			
	return false
}
function getSelectedValue(value){
	if(value){
		return value
	}			
	
	return "N/A"
}

function onlyNumbers(evt) {
  var theEvent = evt || window.event;

  // Handle paste
  if (theEvent.type === 'paste') {
	  key = event.clipboardData.getData('text/plain');
  } else {
  // Handle key press
	  var key = theEvent.keyCode || theEvent.which;
	  key = String.fromCharCode(key);
  }
  var regex = /[0-9]|\./;
  if( !regex.test(key) ) {
	theEvent.returnValue = false;
	if(theEvent.preventDefault) theEvent.preventDefault();
  }
}
	