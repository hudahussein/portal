<template>
  <div class="Courses internal-page" id="Vacation_Request">
    <!-- <h1>
      <img src="assets/layout/img/course.png" alt="courses" />
      Vacation Request Form
    </h1>-->

    <div>{{ adminusersData }}</div>

    <div class="custom-form create-course">
      <div class="cu-container">
        <div class="cu-form-group special shadow">
          <div class="title">
            <span class="bg-secondary">Vacation Request</span>
          </div>

          <div class="grouped shadow-sm rounded">
            <div class="row">
              <!-- employee id -->
              <div class="cu-field col-md-6">
                <h3 class="cu-label">
                  <label>Employee Id :</label>
                </h3>
                <div class="cu-input text-box">
                  <span class="fa fa-edit"></span>
                  <select
                    id="Employees"
                    class="form-control"
                    v-model="impId"
                    @change="NewHR_Vacations_Requests.Delegation_EmpName = ''"
                  >
                    <option value="" selected="selected">..</option>
                    <option v-for="user in adminusersData" :value="user.Emp_ID">
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
              <div class="cu-field col-md-6">
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
          <div class="grouped shadow-sm rounded">
            <div class="cu-field6">
              <h3 class="cu-label">
                <label>Delegated To :</label>
              </h3>
              <select
                class="form-control f-select"
                v-model="NewHR_Vacations_Requests.Delegation_EmpName"
              >
                <option value="0" selected="selected">..</option>
                <option
                  v-for="user in adminusersData"
                  :key="user.Emp_ID"
                  v-if="user.Emp_ID != impId"
                  :value="user.FullName"
                  @click="NewHR_Vacations_Requests.Delegation_EmpCode = user.Emp_ID"
                >
                  {{ user.FullName }}
                </option>
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
                      <input
                        type="radio"
                        class="hidden-inputs"
                        name="Q00"
                        value="Paid"
                        v-model="NewHR_Vacations_Requests.Deduction_name"
                      />
                      <span class="input-group-addon"></span>
                      <span class="input-group-title">Paid</span>
                    </label>
                    <label
                      class="input-group input-group-radio rounded col-md-6"
                      @click="
                        NewHR_Vacations_Requests.Vacation_Type_name = '';
                        NewHR_Vacations_Requests.Vacation_Type_Others = '';
                      "
                    >
                      <input
                        type="radio"
                        class="hidden-inputs"
                        name="Q00"
                        value="Unpaid"
                        v-model="NewHR_Vacations_Requests.Deduction_name"
                        @click="NewHR_Vacations_Requests.Vacation_Type_id = 500"
                      />
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
            <div
              class="popup-body"
              v-if="NewHR_Vacations_Requests.Deduction_name == 'Paid'"
            >
              <fieldset class="required">
                <h3 class="cu-label">
                  <label>Purpose for Time-Off :</label>
                </h3>
                <div class="pt-2 pb-2">
                  <div class="label-cont row">
                    <label
                      class="input-group input-group-radio rounded col-md-6"
                      v-for="Vacation in Vacations"
                      :key="Vacation.id"
                    >
                      <input
                        type="radio"
                        class="hidden-inputs"
                        name="Q000"
                        :value="Vacation.Type_Name.trim()"
                        v-model="NewHR_Vacations_Requests.Vacation_Type_name"
                        @click="NewHR_Vacations_Requests.Vacation_Type_id = Vacation.id"
                      />
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
                <input
                  type="text"
                  v-model="NewHR_Vacations_Requests.Vacation_Type_Others"
                />
              </div>
            </div>
          </div>

          <!-- calender -->
          <div
            class="grouped shadow-sm rounded"
            v-show="oneCalendar.includes(NewHR_Vacations_Requests.Vacation_Type_name)"
          >
            <div class="row">
              <div class="col-md-12 d-flex justify-content-center">
                <div id="calendar" class="m-auto"></div>
              </div>
            </div>
          </div>

          <!-- start and end dates -->
          <div
            class="grouped shadow-sm rounded"
            v-show="
              twoCalendar.includes(NewHR_Vacations_Requests.Vacation_Type_name) ||
              NewHR_Vacations_Requests.Deduction_name == 'Unpaid'
            "
          >
            <div class="row">
              <!-- start date -->
              <div class="col-md-6">
                <div class="cu-field">
                  <label class="cu-label">
                    <span>Start On :</span>
                  </label>
                  <div class="cu-input text-box">
                    <span class="fa fa-calendar"></span>
                    <input
                      type="date"
                      v-model="NewHR_Vacations_Requests.Start_date"
                      :min="new Date().toISOString().substring(0, 10)"
                    />
                  </div>
                </div>
              </div>

              <!-- End Date -->
              <div class="col-md-6">
                <div class="cu-field">
                  <label class="cu-label">
                    <span>End On :</span>
                  </label>
                  <div class="cu-input text-box">
                    <span class="fa fa-calendar"></span>
                    <input
                      type="date"
                      :min="NewHR_Vacations_Requests.Start_date"
                      v-model="NewHR_Vacations_Requests.End_date"
                    />
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
          <div
            class="grouped shadow-sm rounded"
            v-show="NewHR_Vacations_Requests.Vacation_Type_id == '103'"
          >
            <div class="cu-field">
              <label class="cu-label"><span>Off Days :</span></label>
              <div data-pop-name="days-popup" class="select-multi open-pop">
                Select Days
              </div>
              <div class="days-popup popup multi-popup">
                <div class="popup-body bg-light rounded p-3 mt-3">
                  <span class="close-pop">Close</span>
                  <div class="cu-form-group special">
                    <div v-if="daysOff.length > 0">
                      <p class="bg-light rounded p-2">
                        <i class="fa fa-info-circle text-info mr-1"></i> Choose a day and
                        select from the calendar the day you want to take as a day off!
                      </p>
                      <ul class="list-unstyled scroll-box">
                        <li class="border-top" v-for="(day, i) in daysOff" :key="day.id">
                          <div class="container">
                            <div class="row">
                              <div class="col-md-6">
                                <div class="cu-field">
                                  <div class="radio-group">
                                    <input
                                      :id="'p' + i"
                                      type="radio"
                                      name="day"
                                      :value="day.id"
                                      @change="offDay.chosen = ''"
                                      v-model="offDay.checked"
                                    />
                                    <label :for="'p' + i">
                                      <div class="cu-input cu-radio normal">
                                        <div class="contain">
                                          <span class="fa fa-check"></span>
                                          <div>{{ day.Date_From }}</div>
                                        </div>
                                      </div>
                                    </label>
                                  </div>
                                </div>
                              </div>
                              <div class="col-md-6">
                                <div class="cu-field">
                                  <div class="cu-input text-box">
                                    <span class="fa fa-calendar"></span>
                                    <input
                                      type="date"
                                      :min="
                                        new Date(new Date().toISOString().substr(0, 10)) >
                                        new Date(day.Date_From.trim())
                                          ? new Date().toISOString().substr(0, 10)
                                          : new Date(
                                              new Date(day.Date_From.trim()).getTime() +
                                                24 * 60 * 60 * 1000
                                            )
                                              .toISOString()
                                              .substr(0, 10)
                                      "
                                      @change="offDay.chosen = $event.target.value"
                                    />
                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>
                        </li>
                      </ul>
                    </div>
                    <div v-else class="alert alert-danger text-center mt-3 mb-3">
                      Sorry, This employee has no off days!
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- attachment file -->
          <div
            class="grouped shadow-sm rounded Courses"
            v-show="
              attach.includes(NewHR_Vacations_Requests.Vacation_Type_name) ||
              NewHR_Vacations_Requests.Deduction_name == 'Unpaid'
            "
          >
            <h3 class="cu-label">
              <label>Please attach file here:</label>
            </h3>
            <div class="cu-input text-box">
              <div class="custom-file">
                <label class="custom-file-label" for="fuSample">
                  {{
                    NewHR_Vacations_Requests.Attach_File != ""
                      ? NewHR_Vacations_Requests.Attach_File.split("\\").slice(-1)[0]
                      : "Choose file..."
                  }}
                </label>
                <asp:FileUpload
                  runat="server"
                  ID="fuSample"
                  v-model="NewHR_Vacations_Requests.Attach_File"
                  class="custom-file-input"
                />
                <asp:Button
                  runat="server"
                  ID="btnUpload"
                  Text="Upload"
                  onclick="btnUpload_Click"
                  class="uploadBtn HideUpload"
                />
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
                      <input
                        type="radio"
                        class="hidden-inputs"
                        name="Qall000"
                        value="Phone"
                        v-model="NewHR_Vacations_Requests.Emergency_Contact"
                      />
                      <span class="input-group-addon"></span>
                      <span class="input-group-title">Phone</span>
                    </label>
                    <label class="input-group input-group-radio rounded col-md-4">
                      <input
                        type="radio"
                        class="hidden-inputs"
                        name="Qall000"
                        value="Email"
                        v-model="NewHR_Vacations_Requests.Emergency_Contact"
                      />
                      <span class="input-group-addon"></span>
                      <span class="input-group-title">Email</span>
                    </label>
                    <label class="input-group input-group-radio rounded col-md-4">
                      <input
                        type="radio"
                        class="hidden-inputs"
                        name="Qall000"
                        value="both phone or mail"
                        v-model="NewHR_Vacations_Requests.Emergency_Contact"
                      />
                      <span class="input-group-addon"></span>
                      <span class="input-group-title">both</span>
                    </label>
                  </div>
                </div>
              </fieldset>
            </div>
          </div>

          <!-- Employee comment -->
          <div class="grouped shadow-sm rounded">
            <div class="row">
              <!-- employee name -->
              <div class="cu-field col-md-12">
                <h3 class="cu-label">
                  <label>Employee Comment :</label>
                </h3>
                <div class="cu-input text-box" style="max-width: 100%">
                  <span class="fa fa-edit"></span>
                  <textarea
                    v-model="NewHR_Vacations_Requests.Requester_Comment"
                  ></textarea>
                </div>
              </div>
            </div>
          </div>

          <!-- submit-->
          <div class="text-center pt-3 pb-3">
            <button
              class="btn btn-secondary"
              type="button"
              v-on:click="SubmitVacationRequest"
            >
              Send Request
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "vacationRequest",
  data() {
    return {
      adminusersData: null,
      Vacations: null,
      daysOff: [],

      impId: "",
      impName: "",
      impDept: "",
      impBranch: "",

      startDate: "",
      endDate: "",
      totalDayes: 0,

      //filters
      oneCalendar: ["Casual", "Annual", "Mariage", "PH"],
      twoCalendar: ["Sick Leave", "Death", "Military", "Working Injury", "Maternity"],
      attach: [
        "Death",
        "Military",
        "Working Injury",
        "Mariage",
        "Sick Leave",
        "Maternity",
      ],

      multiDates: [],
      offDay: { chosen: "", checked: "" },
      Employees_Balance: [],

      NewHR_Vacations_Requests: {
        Requester_id: 1,
        Delegation_EmpCode: 0,
        Delegation_EmpName: "",
        Deduction_id: 1,
        Deduction_name: "Paid",
        Vacation_Type_id: 1,
        Vacation_Type_name: "",
        Vacation_Type_Others: "",
        Start_date: "",
        End_date: "",
        Total_Days: 1,
        Emergency_Contact: "",
        Requester_Comment: "",
        Attach_File: "",
        OFF_Day: 0,
        Status: 1,
      },
      apiUrl: "http://localhost:50567/app/dist/",
    };
  },

  methods: {
    offDays: function (impId) {
      let that = this;

      //get daysOff
      if (impId !== "") {
        $.ajax({
          type: "POST",
          url: that.apiUrl + "Hr_Portal/Vacation_Request.aspx/getDaysOffData",
          data: JSON.stringify({ detail: { Emp_id: impId } }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            that.daysOff = JSON.parse(data.d);
          },
        });
      } else {
        that.daysOff = [];
      }
    },

    checkDay: function (exp, day) {
      return (new Date(exp) - new Date(day)) / (24 * 60 * 60 * 1000) >= 0;
    },
    SubmitVacationRequest: function () {
      var ObjectD = Object.assign({}, this.NewHR_Vacations_Requests);

      let that = this,
        today = new Date().toISOString().substring(0, 10),
        type = ObjectD.Vacation_Type_name.trim(),
        Days = that.multiDates,
        DaysNum = Days.length;
      ObjectD.Requester_id = that.impId;

      // get multi dates from calendar
      this.multiDates = Array.from(new Set(this.multiDates));

      if (that.impId === "") {
        swal({
          title: "Error!",
          text: "Sorry, you should choose the employee Id ! ",
          icon: "warning",
          dangerMode: true,
        });
      } else if (ObjectD.Delegation_EmpName == "0" || ObjectD.Delegation_EmpName == "") {
        swal({
          title: "Error!",
          text:
            "Sorry, you should choose a colleague to delegate the job to while you are absent! ",
          icon: "warning",
          dangerMode: true,
        });
      } else if (ObjectD.Deduction_name == "") {
        swal({
          title: "Error!",
          text: "Sorry, you should choose the type of your vacation ! ",
          icon: "warning",
          dangerMode: true,
        });
      } else if (type == "" && ObjectD.Vacation_Type_Others == "") {
        swal({
          title: "Error!",
          text:
            "Sorry, you should choose the purpose of the vacation or type another reason ! ",
          icon: "warning",
          dangerMode: true,
        });
      } else if (
        (that.twoCalendar.includes(type) || ObjectD.Deduction_name == "Unpaid") &&
        !that.checkDay(ObjectD.End_date, ObjectD.Start_date)
      ) {
        swal({
          title: "Error!",
          text: "Sorry, the start date is after the end date ! ",
          icon: "warning",
          dangerMode: true,
        });
      } else if (ObjectD.Emergency_Contact == "") {
        swal({
          title: "Error!",
          text:
            "Sorry, you should choose how to contact you during the vacation period ! ",
          icon: "warning",
          dangerMode: true,
        });
      } else {
        // check dates with two calendars
        if (that.twoCalendar.includes(type) || ObjectD.Deduction_name == "Unpaid") {
          if (ObjectD.Start_date == 0) {
            swal({
              title: "Error!",
              text: "Sorry you should pick the start date of your vacation ! ",
              icon: "warning",
              dangerMode: true,
            });
            return false;
          } else if (ObjectD.End_date == 0) {
            swal({
              title: "Error!",
              text: "Sorry, you should pick the end date of your vacation! ",
              icon: "warning",
              dangerMode: true,
            });
            return false;
          } else if (!that.checkDay(ObjectD.Start_date, today)) {
            swal({
              title: "Error!",
              text: "Sorry, You can't pick a date before today's date!",
              icon: "warning",
              dangerMode: true,
            });
            return false;
          } else {
            that.multiDates = [
              { Date_from: ObjectD.Start_date, Date_to: ObjectD.End_date },
            ];
          }
        }

        // check dates with one calendar
        if (that.oneCalendar.includes(type)) {
          that.totalDayes = that.multiDates.length;

          if (that.multiDates.length == 0) {
            swal({
              title: "Error!",
              text: "Sorry, you should pick the date of your vacation ! ",
              icon: "warning",
              dangerMode: true,
            });
            return false;
          } else {
            // check if all the dates are valid
            for (let i = 0; i < that.multiDates.length; i++) {
              if (!that.checkDay(that.multiDates[i].Date_from, today)) {
                swal({
                  title: "Error!",
                  text: "Sorry, You can't pick a date before today's date! ",
                  icon: "warning",
                  dangerMode: true,
                });
                return false;
              }
            }
          }
        }

        // check attachment file
        if (
          that.attach.includes(ObjectD.Vacation_Type_name) ||
          ObjectD.Deduction_name == "Unpaid"
        ) {
          if (ObjectD["Attach_File"] == "") {
            swal({
              title: "Error!",
              text: "Sorry, you should attach a file! ",
              icon: "warning",
              dangerMode: true,
            });
            return false;
          } else {
            ObjectD["Attach_File"] = ObjectD["Attach_File"].split("\\").slice(-1)[0];
          }
        } else {
          ObjectD["Attach_File"] = "";
        }

        // check dates with off day
        if (ObjectD.Vacation_Type_id == 103) {
          // validate the off day that has been checked
          $.ajax({
            type: "POST",
            url: that.apiUrl + "Hr_Portal/Vacation_Request.aspx/getDaysOffData",
            data: JSON.stringify({ detail: { Emp_id: that.impId } }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
              let checkedDay = JSON.parse(data.d).filter(
                (x) => x.id == that.offDay.checked
              )[0];
              if (that.offDay.chosen == "") {
                swal({
                  title: "Error!",
                  text: "Sorry you should pick the day you want to take as an off day! ",
                  icon: "warning",
                  dangerMode: true,
                });
                return false;
              } else if (that.offDay.checked == "") {
                swal({
                  title: "Error!",
                  text: "Sorry, you should check on one of your off days first! ",
                  icon: "warning",
                  dangerMode: true,
                });
                return false;
              } else if (!checkedDay) {
                swal({
                  title: "Error!",
                  text: "Sorry, The day you picked as an off day is not found!",
                  icon: "warning",
                  dangerMode: true,
                });
                return false;
              } else if (
                new Date(that.offDay.chosen) < new Date() ||
                new Date(that.offDay.chosen) < new Date(checkedDay)
              ) {
                swal({
                  title: "Error!",
                  text:
                    "Sorry, You can't pick a date before today's date or the day you checked!",
                  icon: "warning",
                  dangerMode: true,
                });
                return false;
              } else if (
                (new Date(new Date().getTime() + 31 * 24 * 60 * 60 * 1000) -
                  new Date(that.offDay.chosen)) /
                  (30 * 24 * 60 * 60 * 1000) <
                0
              ) {
                swal({
                  title: "Error!",
                  text: "Sorry, You can't pick a date after 30 days of the off day!",
                  icon: "warning",
                  dangerMode: true,
                });
                return false;
              } else {
                that.multiDates = [
                  { Date_from: that.offDay.chosen, Date_to: that.offDay.chosen },
                ];
                ObjectD.OFF_Day = that.offDay.checked;
                that.totalDayes = that.multiDates.length;
                sending();
              }
            },
          });
        }

        // checking balance
        else if (type == "Annual" || type == "Casual") {
          $.ajax({
            type: "POST",
            url: that.apiUrl + "Hr_Portal/Vacation_Request.aspx/checkBalance",
            data: JSON.stringify({ detail: { Emp_id: ObjectD.Requester_id } }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
              that.Employees_Balance = JSON.parse(data.d)[0];
              if (that.Employees_Balance) {
                let totalBalance = that.Employees_Balance.Annual_Balance,
                  casualBalance = that.Employees_Balance.Casual_Balance,
                  expOldBalance = that.Employees_Balance["Old_Balance_ExpirationDate"],
                  minusOldBalance = that.Employees_Balance["Old_Balance"],
                  currentMonth = new Date().getMonth() + 1;

                // if the type is annual check the days that before the expiration date of the old balance
                if (that.checkDay(expOldBalance, today) && type == "Annual") {
                  for (let i = 0; i < Days.length; i++) {
                    if (
                      that.checkDay(expOldBalance, Days[i].Date_from) &&
                      minusOldBalance > 0
                    ) {
                      minusOldBalance--;
                      DaysNum--;
                    }
                  }
                  if (DaysNum == 0) {
                    sending();
                  }
                }

                // if the old balance is not enough we start decrease the totalBalance
                if (DaysNum > 0) {
                  if (currentMonth < 4) {
                    // q1
                    let total = that.Employees_Balance["Q1"];
                    decrease(total);
                  } else if (currentMonth < 7) {
                    // q2
                    let total =
                      that.Employees_Balance["Q1"] + that.Employees_Balance["Q2"];
                    decrease(total);
                  } else if (currentMonth < 10) {
                    // q3
                    let total =
                      that.Employees_Balance["Q1"] +
                      that.Employees_Balance["Q2"] +
                      that.Employees_Balance["Q3"];
                    decrease(total);
                  } else {
                    // q4
                    let total =
                      that.Employees_Balance["Q1"] +
                      that.Employees_Balance["Q2"] +
                      that.Employees_Balance["Q3"] +
                      that.Employees_Balance["Q4"];
                    decrease(total);
                  }
                }

                //decreasing the balance
                function decrease(total) {
                  // if the vacation is casual
                  if (type == "Casual") {
                    if (DaysNum > casualBalance || DaysNum > total) {
                      alert();
                    } else {
                      // check if the requester had requested casual vacation more than two times in the same month
                      $.ajax({
                        type: "POST",
                        url:
                          that.apiUrl + "Hr_Portal/Vacation_Request.aspx/getPeriodsData",
                        data: JSON.stringify({
                          detail: { Emp_id: ObjectD.Requester_id },
                        }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                          let Dates = JSON.parse(data.d).map((x) => x["Date_From"]);
                          if (Dates.length == 0) {
                            sending();
                          }
                          for (let i = 0; i < Days.length; i++) {
                            let month =
                              new Date(Days[i]["Date_from"].trim()).getMonth() + 1;

                            if (
                              Dates.filter(
                                (x) => new Date(x.trim()).getMonth() + 1 == month
                              ).length >= 2
                            ) {
                              swal({
                                title: "Not Allowed!",
                                text:
                                  "Sorry, You can't have casual vacation more than two days in the same month!",
                                icon: "warning",
                                dangerMode: true,
                              });
                              return false;
                            } else {
                              if (i == Days.length - 1) {
                                sending();
                              }
                            }
                          }
                        },
                      });
                    }
                  } else {
                    return total >= DaysNum ? sending() : alert();
                  }
                }
              } else {
                swal({
                  title: "No Balance Found!",
                  text: "Sorry, there was a problem finding your balance!",
                  icon: "warning",
                  dangerMode: true,
                });
                return false;
              }
            },
          });
          //alert if the balance is not enough
          function alert() {
            swal({
              title: "Not Allowed!",
              text:
                "The current vacation balance for this Employee is not enough to accept this request!",
              icon: "warning",
              dangerMode: true,
            });
            return false;
          }
        } else {
          sending();
        }

        /******/

        function sending() {
          ObjectD.Total_Days = parseInt(that.totalDayes);

          swal({
            title: "Sending...",
            text: "Please wait",
            showConfirmButton: false,
            allowOutsideClick: false,
          });

          $.ajax({
            type: "POST",
            url: that.apiUrl + "Hr_Portal/Vacation_Request.aspx/NewHR_Vacations_Requests",
            data: JSON.stringify({ HR_Vacations_Request: ObjectD }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
              function ajaxFunc() {
                if (ObjectD.Vacation_Type_id == 103) {
                  // update off days
                  return $.ajax({
                    type: "POST",
                    url: that.apiUrl + "Hr_Portal/Vacation_Request.aspx/updateOffDays",
                    data: JSON.stringify({
                      updateDays: { id: that.offDay.checked, Status: 6 },
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                  });
                } else {
                  return true;
                }
              }
              $.when(ajaxFunc()).done(function (a) {
                for (let i = 0; i < that.multiDates.length; i++) {
                  $.ajax({
                    type: "POST",
                    url:
                      that.apiUrl +
                      "Hr_Portal/Vacation_Request.aspx/NewHR_Vacations_Days",
                    data: JSON.stringify({ Vacations_Days: that.multiDates[i] }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                      if (i == that.multiDates.length - 1) {
                        swal({
                          title: "Done!",
                          text: "You successfully added vacation request ...",
                          icon: "success",
                        }).then((result) => {
                          if (result) {
                            if (ObjectD["Attach_File"] != "") {
                              $(".uploadBtn").click();
                            } else {
                              location.reload();
                            }
                          }
                        });
                      }
                    },
                    error: function () {
                      swal({
                        title: "Error!",
                        text: "Something went wrong, please try again! ",
                        icon: "warning",
                        dangerMode: true,
                      });
                    },
                  }); // end of inserting days by ajax
                }
              });
            },
            error: function () {
              swal({
                title: "Error!",
                text: "Something went wrong, please try again! ",
                icon: "warning",
                dangerMode: true,
              });
            },
          }); // end of inserting request by ajax
        }
      }
    },
  },

  watch: {
    impId: function () {
      this.impBranch = this.adminusersData.filter(
        (x) => x.Emp_ID == this.impId
      )[0].Branch_name;
      this.impName = this.adminusersData.filter(
        (x) => x.Emp_ID == this.impId
      )[0].FullName;
      this.impDept = this.adminusersData.filter(
        (x) => x.Emp_ID == this.impId
      )[0].Dept_name;
      this.offDays(this.impId);
    },

    "NewHR_Vacations_Requests.End_date": function () {
      let dateFrom = this.NewHR_Vacations_Requests.Start_date,
        dateTo = this.NewHR_Vacations_Requests.End_date;

      if (this.NewHR_Vacations_Requests.Start_date.length > 0) {
        this.totalDayes = (new Date(dateTo) - new Date(dateFrom)) / 86400000 + 1;
      }
    },
  },
  created: function () {
    let cobject = this;

    //get adminusers
    $.ajax({
      type: "POST",
      url: cobject.apiUrl + "Hr_Portal/Vacation_Request.aspx/getadminusersData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        cobject.adminusersData = JSON.parse(data.d);
      },
    });

    //get Vacation types
    $.ajax({
      type: "POST",
      url: cobject.apiUrl + "Hr_Portal/Vacation_Request.aspx/gettypesData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        cobject.types = JSON.parse(data.d);
        cobject.Vacations = cobject.types;
      },
    });

    // get multi dates from calendar
    $("body").on("click", ".ui-state-default", function (e) {
      e.preventDefault();
      let date =
        $(this).closest("td").data("year") +
        "-" +
        (parseInt($(this).closest("td").data("month")) + 1) +
        "-" +
        $(this).text();

      cobject.multiDates.push({ Date_from: date, Date_to: date });
    });

    $("body").on("click", ".ui-datepicker-clear-month", function () {
      cobject.multiDates = [];
    });
  },
};
</script>

<style></style>
