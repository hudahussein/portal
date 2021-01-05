<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Vacation_Request.aspx.cs"  Inherits="_Vacation_Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<style>
		.HideUpload {
			opacity:0;
			height:0 !important;
			margin:0 !important;
		}
		.select-multi {
			background-color: #fff;
			padding: 5px 15px;
			border-radius: 4px;
			color: #666;
			text-align: center;
			border: 1px solid #e0e0e0;
			transition: all .5s ease;
			cursor: pointer;
		}
		.select-multi:hover {
			background-color: #e0e0e0;
		}
		.ui-datepicker-clear-month {
			position: absolute;
			top: 9px;
			right: 40px;
			height: 100%;
			line-height: 100%;
			display: inline;
			cursor: pointer;
			color: red!important;
		}
		.ui-state-active, .ui-widget-content .ui-state-active{
			background: #666 !important;
			color: #fff !important;
		}
		.ui-datepicker {
			width: 35em !important;
			padding: .2em .2em 0;
			display: none;
		}
		.ui-datepicker th {
			padding: .4em .2em !important;
		}
		.ui-datepicker td a {
			text-align: center !important;
		}
		.ui-widget {
			font-family: inherit !important;
			font-size: 1em !important;
		}
	</style>

	<div class='Courses internal-page' id='Vacation_Request'>
		<h1>
			<img src='../layout/img/course.png' alt='courses'/>
			Vacation Request Form
		</h1>

		<div class="custom-form create-course">
			<div class="cu-container">

				<div class="cu-form-group special shadow">

					<div class="title">
						<span class="bg-secondary">Vacation Request</span>
					</div>

					<div class='grouped shadow-sm rounded'>
						<div class='row'>

							<!-- employee id -->
							<div class="cu-field col-md-6">
								<h3 class="cu-label">
									<label>Employee Id :</label>
								</h3>
								<div class="cu-input text-box">
									<span class="fa fa-edit"></span>
									<select id="Employees" class="form-control"
											v-model="impId" @change='NewHR_Vacations_Requests.Delegation_EmpName = ""'>
										<option value="" selected="selected">..</option>
										<option  v-for='user in adminusersData'
												:value='user.Emp_ID'>
											{{ user.Emp_ID }}
										</option>
									</select>
								</div>
							</div>

							<!-- employee name -->
							<div class="cu-field col-md-6">
								<h3 class="cu-label">
									<label>Employee Name :</label>
								</h3>
								<div class="cu-input text-box">
									<span class="fa fa-edit"></span>
									<div>{{ impName }}</div>
								</div>
							</div>

							<!-- department -->
							<div class="cu-field col-md-6">
								<h3 class="cu-label">
									<label>Department Name :</label>
								</h3>
								<div class="cu-input text-box">
									<span class="fa fa-edit"></span>
									<div>{{ impDept }}</div>
								</div>
							</div>

							<!-- Branch Name -->
							<div class="cu-field  col-md-6">
								<h3 class="cu-label">
									<label>Branch Name :</label>
								</h3>
								<div class="cu-input text-box">
									<span class="fa fa-edit"></span>
									<div>{{ impBranch }}</div>
								</div>
							</div>
						</div>
					</div>
					
					<!-- elegated to -->
					<div class='grouped shadow-sm rounded'>
						<div class="cu-field6">
							<h3 class="cu-label">
								<label>Delegated To :</label>
							</h3>
							<select class='form-control f-select' v-model='NewHR_Vacations_Requests.Delegation_EmpName'>
								<option value="0" selected="selected">..</option>
								<option v-for='user in adminusersData'
										v-if='user.Emp_ID != impId'
										:value='user.FullName'
										@click='NewHR_Vacations_Requests.Delegation_EmpCode = user.Emp_ID'>{{ user.FullName }}</option>
							</select>
						</div>
					</div>

					<!-- time off type -->	
					<div class="grouped shadow-sm rounded Courses">
						<div class="popup-body">
							<fieldset class="required">
								<h3 class="cu-label">
									<label>Time-Off type :</label>
								</h3>
								<div class="pt-2 pb-2">
									<div class="label-cont row">
										<label class="input-group input-group-radio rounded col-md-6">
											<input type="radio" class="hidden-inputs" name="Q00" value="Paid" v-model="NewHR_Vacations_Requests.Deduction_name">
											<span class="input-group-addon"></span>
											<span class="input-group-title">Paid</span>
										</label>
										<label class="input-group input-group-radio rounded col-md-6" 
											   @click='NewHR_Vacations_Requests.Vacation_Type_name = "";NewHR_Vacations_Requests.Vacation_Type_Others = ""'>
											<input type="radio" class="hidden-inputs" name="Q00" value="Unpaid" v-model="NewHR_Vacations_Requests.Deduction_name" @click='NewHR_Vacations_Requests.Vacation_Type_id = 500'>
											<span class="input-group-addon"></span>
											<span class="input-group-title">Unpaid</span>
										</label>
									</div>
								</div>			  
							</fieldset>
						</div>
					</div>


					<!-- Purpose of Time-Off -->	
					<div class="grouped shadow-sm rounded Courses">
						<div class="popup-body"  v-if='NewHR_Vacations_Requests.Deduction_name == "Paid"'>
							<fieldset class="required">
								<h3 class="cu-label">
									<label>Purpose for Time-Off :</label>
								</h3>
								<div class="pt-2 pb-2">
									<div class="label-cont row">
										<label class="input-group input-group-radio rounded col-md-6" v-for="Vacation in Vacations">
											<input type="radio" class="hidden-inputs" name="Q000" :value="Vacation.Type_Name.trim()" v-model="NewHR_Vacations_Requests.Vacation_Type_name" @click='NewHR_Vacations_Requests.Vacation_Type_id = Vacation.id'>
											<span class="input-group-addon"></span>
											<span class="input-group-title">{{ Vacation.Type_Name }}</span>
										</label>
									</div>
								</div>			  
							</fieldset>
						</div>

						<!-- other reason -->
						<div class="cu-field" v-else>
							<h3 class="cu-label">
								<label>Reason :</label>
							</h3>
							<div class="cu-input text-box">
								<span class="fa fa-edit"></span>
								<input type="text" v-model="NewHR_Vacations_Requests.Vacation_Type_Others">
							</div>
						</div>
					</div>
					
					
					<!-- calender -->
					<div class='grouped shadow-sm rounded' v-show='oneCalendar.includes(NewHR_Vacations_Requests.Vacation_Type_name)'>
						<div class='row'>
							<div class="col-md-12 d-flex justify-content-center">
								<div id="calendar" class="m-auto"></div>
							</div>
						</div>
					</div>
					
					<!-- start and end dates -->
					<div class='grouped shadow-sm rounded' v-show='twoCalendar.includes(NewHR_Vacations_Requests.Vacation_Type_name) || NewHR_Vacations_Requests.Deduction_name == "Unpaid"'>
						<div class='row'>
							<!-- start date -->
							<div class='col-md-6'>
								<div class="cu-field">
									<label class="cu-label">
										<span>Start On :</span>
									</label>
									<div class="cu-input text-box">
										<span class="fa fa-calendar"></span>
										<input type='date' v-model="NewHR_Vacations_Requests.Start_date" :min="new Date().toISOString().substring(0,10)">
									</div>
								</div>
							</div>

							<!-- End Date -->
							<div class='col-md-6'>
								<div class="cu-field">
									<label class="cu-label">
										<span>End On :</span>
									</label>
									<div class="cu-input text-box">
										<span class="fa fa-calendar"></span>
										<input type='date' :min="NewHR_Vacations_Requests.Start_date" v-model="NewHR_Vacations_Requests.End_date">
									</div>
								</div>
							</div>

							<!-- Total Days -->
							<div class="cu-field col-md-12">
								<h3 class="cu-label">
									<label>Total Days :</label>
								</h3>
								<div class="cu-input text-box bg-white" style="max-width: 100%">
									<span class="fa fa-edit" style="background-color: #e0e0e0"></span>
									<div class="pl-3">{{ totalDayes }} days</div>
								</div>
							</div>	

						</div>
					</div>

					<!-- off days -->
					<div class='grouped shadow-sm rounded' v-show='NewHR_Vacations_Requests.Vacation_Type_id == "103"'>
						<div class="cu-field">
							<label class="cu-label"><span>Off Days :</span></label> 
							<div data-pop-name="days-popup" class="select-multi open-pop">Select Days</div>
								<div class="days-popup popup multi-popup">
									<div class="popup-body bg-light rounded p-3 mt-3">
										<span class="close-pop">Close</span> 
										<div class="cu-form-group special">
											<div v-if='daysOff.length > 0'>
												<p class='bg-light rounded p-2'> <i class='fa fa-info-circle text-info mr-1'></i>
													Choose a day and select from the calendar the day you want to take as a day off!</p>
												<ul class="list-unstyled scroll-box">
													
													<li class='border-top' v-for='(day, i) in daysOff'>
														<div class='container'>
															<div class='row'>
																<div class='col-md-6'>
																	<div class="cu-field">
																		<div class="radio-group">
																			<input :id="'p' + i" type="radio" name="day" :value="day.id"
																				   @change='offDay.chosen = ""'
																				   v-model='offDay.checked'> 
																			<label :for="'p' + i">
																				<div class="cu-input cu-radio normal">
																					<div class="contain">
																						<span class="fa fa-check"></span> 
																						<div>{{day.Date_From}}</div>
																					</div>
																				</div>
																			</label>
																		</div>
																	</div>
																</div>
																<div class='col-md-6'>
																	<div class="cu-field">
																		<div class="cu-input text-box">
																			<span class="fa fa-calendar"></span>
																			<input type='date' 
																				   :min="new Date(new Date().toISOString().substr(0,10)) >
																						 new Date(day.Date_From.trim()) ? new Date().toISOString().substr(0,10)
																						 : new Date(new Date(day.Date_From.trim()).getTime() + 
																						 (24*60*60*1000)).toISOString().substr(0,10)"
																				   @change='offDay.chosen = $event.target.value'/>
																		</div>
																	</div>
																</div>
															</div>
														</div>
													</li>
												</ul>
											</div>
											<div v-else class='alert alert-danger text-center mt-3 mb-3'>Sorry, This employee has no off days!</div>
										</div>
									</div>
								</div>
						</div>
					</div>

					<!-- attachment file -->
					<div class="grouped shadow-sm rounded Courses" v-show='attach.includes(NewHR_Vacations_Requests.Vacation_Type_name) || NewHR_Vacations_Requests.Deduction_name == "Unpaid"'>
						<h3 class="cu-label">
							<label>Please attach file here:</label>
						</h3>
						<div class="cu-input text-box">
							<div class='custom-file'>
								<label class="custom-file-label" for="fuSample">
									{{NewHR_Vacations_Requests.Attach_File != '' ?
									NewHR_Vacations_Requests.Attach_File.split('\\').slice(-1)[0] : 'Choose file...'}}
								</label>
								<asp:FileUpload  runat="server" ID="fuSample" v-model="NewHR_Vacations_Requests.Attach_File" class='custom-file-input' />
								<asp:Button  runat="server" ID="btnUpload" Text="Upload" onclick="btnUpload_Click" class='uploadBtn HideUpload' />
							</div>
						</div>
					</div>

					<!-- Emergency cases -->	
					<div class="grouped shadow-sm rounded Courses">
						<div class="popup-body">
							<fieldset class="required">
								<h3 class="cu-label">
									<label>In case of emergency, How can we contact you ?</label>
								</h3>
								<div class="pt-2 pb-2">
									<div class="label-cont row">
										<label class="input-group input-group-radio rounded col-md-4">
											<input type="radio" class="hidden-inputs" name="Qall000" value="Phone" v-model="NewHR_Vacations_Requests.Emergency_Contact">
											<span class="input-group-addon"></span>
											<span class="input-group-title">Phone</span>
										</label>
										<label class="input-group input-group-radio rounded col-md-4">
											<input type="radio" class="hidden-inputs" name="Qall000" value="Email" v-model="NewHR_Vacations_Requests.Emergency_Contact">
											<span class="input-group-addon"></span>
											<span class="input-group-title">Email</span>
										</label>
										<label class="input-group input-group-radio rounded col-md-4">
											<input type="radio" class="hidden-inputs" name="Qall000" value="both phone or mail" v-model="NewHR_Vacations_Requests.Emergency_Contact">
											<span class="input-group-addon"></span>
											<span class="input-group-title">both</span>
										</label>
									</div>
								</div>			  
							</fieldset>
						</div>
					</div>

					<!-- Employee comment -->	
					<div class='grouped shadow-sm rounded'>
						<div class='row'>
							<!-- employee name -->
							<div class="cu-field col-md-12">
								<h3 class="cu-label">
									<label>Employee Comment :</label>
								</h3>
								<div class="cu-input text-box" style="max-width: 100%;">
									<span class="fa fa-edit"></span>
									<textarea v-model="NewHR_Vacations_Requests.Requester_Comment"></textarea>
								</div>
							</div>
						</div>
					</div>



					<!-- submit-->
					<div class='text-center pt-3 pb-3'>
						<button class="btn btn-secondary" type="button" v-on:click="SubmitVacationRequest">Send Request</button>
					</div>

				</div>
			</div>
		</div>

	</div>
	
	<script src='../layout/vue-code/Vacation_Request.js'></script>
</asp:Content>