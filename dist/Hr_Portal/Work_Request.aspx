<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Work_Request.aspx.cs"  Inherits="_Work_Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<style>
		.ui-datepicker-clear-month {
			position: absolute;
			top: 9px;
			right: 32px;
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

	<div class='Courses internal-page' id='Work_Request'>

		<div class="custom-form create-course">
			<div class="cu-container">

				<div class="cu-form-group special p-4">

					<div class="title">
						<span>Work Request</span>
					</div>

					<div class='grouped shadow-sm rounded'>
						<div class='row'>
							<!-- employee name -->
							<div class="cu-field col-md-6">
								<h3 class="cu-label">
									<label>Employee Name :</label>
								</h3>
								<div class="cu-input text-box">
									<span class="fa fa-edit"></span>
									<div><%=Session["admin"]%></div>
								</div>
							</div>

							<!-- employee id -->
							<div class="cu-field col-md-6">
								<h3 class="cu-label">
									<label>Employee Id :</label>
								</h3>
								<div class="cu-input text-box">
									<span class="fa fa-edit"></span>
									<div><%=Session["Emp_ID"]%></div>
								</div>
							</div>

						</div>
					</div>
					
					<div class='grouped shadow-sm pt-4 rounded'>
						<div class="row">
							<!-- Work Request -->
							<div class='col-md-12'>
								<div class="cu-field">
									<label class="cu-label">
										<span>Add work request:</span>
									</label> 
									<!-- calender -->
									<div class="col-md-12 d-flex justify-content-center">
										<div id="calendar" class="m-auto"></div>
									</div>
								</div>
							</div>
						</div>
					</div>

					<!-- Employee comment -->	
					<div class='grouped shadow-sm rounded mb-4'>
						<div class='row'>
							<!-- employee name -->
							<div class="cu-field col-md-12">
								<h3 class="cu-label">
									<label>Employee Comment :</label>
								</h3>
								<div class="cu-input text-box" style="max-width: 100%;">
									<span class="fa fa-edit"></span>
									<textarea v-model="NewHR_DaysOFF_Requests.Requester_Comment"></textarea>
								</div>
							</div>
						</div>
					</div>

					<!-- submit-->
					<div class='text-center'>
						<button class="special-btn m-0" type="button"  v-on:click="SubmitWorkRequest">send request</button>
					</div>


					<div class='grouped shadow-sm pt-3 pb-2 rounded'>
						<div class="row">
							<!-- Display Work Request -->
							<div class='col-md-12'>
								<div class="cu-field overflow-auto" style="height: 250px;">
									<label class="cu-label">
										<span>Work Requests Days :</span>
									</label>
									<!--<div>{{ WorkRequests.filter(x => x.Emp_id == adminusersData[0].admin_id) }}</div>-->
									<table class="table table-striped text-center">
										<thead class="bg-secondary text-white">
											<tr>
												<th scope="col" class="">#</th>
												<th scope="col">Date</th>
												<th scope="col">Status</th>
												<th scope="col">delete</th>
											</tr>
										</thead>
										<tbody>
											<tr v-for="(day, i) in WorkRequests">
												<th>{{ i + 1}}</th>
												<th>{{ day.Date_From }}</th>
												<th v-if="day.Status == 1" class="text-primary">requested</th>
												<th v-else-if="day.Status == 2" class="text-success">Accepted from manger</th>
												<th v-else-if="day.Status == 3" class="text-danger">Rejected from manger</th>
												<th v-else-if="day.Status == 4" class="text-success">Accepted from HR</th>
												<th v-else-if="day.Status == 5" class="text-danger">Rejected from HR</th>
												<th v-else-if="day.Status == 6" class="text-primary">Pending day off request</th>
												<th v-else-if="day.Status == 7" class="text-success">Day off request accepted from manger</th>
												<th v-else-if="day.Status == 8" class="text-success">Day off request accepted from hr</th>
												<th><a href="#">
													<i class="fa fa-trash fa-lg btn text-danger" 
													   aria-hidden="true"
													   @click.prevent="deleteWorkRequest(day.id, true, i)">
													</i>
													</a>
												</th>
											</tr>
										</tbody>
									</table>
								</div>
							</div>
						</div>
					</div>


				</div>
			</div>
		</div>
	</div>
	
	<script src='../layout/vue-code/Work_Request.js'></script>
</asp:Content>