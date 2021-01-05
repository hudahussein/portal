<template>
  <div class="Courses internal-page" id="Work_Request">
    <div class="custom-form create-course">
      <div class="cu-container">
        <div class="cu-form-group special p-4">
          <div class="title">
            <span>Work Request</span>
          </div>

          <div class="grouped shadow-sm rounded">
            <div class="row">
              <!-- employee name -->
              <div class="cu-field col-md-6">
                <h3 class="cu-label">
                  <label>Employee Name :</label>
                </h3>
                <div class="cu-input text-box">
                  <span class="fa fa-edit"></span>
                  <div>{{ user.FullName }}</div>
                </div>
              </div>

              <!-- employee id -->
              <div class="cu-field col-md-6">
                <h3 class="cu-label">
                  <label>Employee Id :</label>
                </h3>
                <div class="cu-input text-box">
                  <span class="fa fa-edit"></span>
                  <div>{{ user.Emp_id }}</div>
                </div>
              </div>
            </div>
          </div>

          <div class="grouped shadow-sm pt-4 rounded">
            <div class="row">
              <!-- Work Request -->
              <div class="col-md-12">
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
          <div class="grouped shadow-sm rounded mb-4">
            <div class="row">
              <!-- employee name -->
              <div class="cu-field col-md-12">
                <h3 class="cu-label">
                  <label>Employee Comment :</label>
                </h3>
                <div class="cu-input text-box" style="max-width: 100%">
                  <span class="fa fa-edit"></span>
                  <textarea v-model="NewHR_DaysOFF_Requests.Requester_Comment"></textarea>
                </div>
              </div>
            </div>
          </div>

          <!-- submit-->
          <div class="text-center">
            <button class="special-btn m-0" type="button" v-on:click="SubmitWorkRequest">
              send request
            </button>
          </div>

          <div class="grouped shadow-sm pt-3 pb-2 rounded">
            <div class="row">
              <!-- Display Work Request -->
              <div class="col-md-12">
                <div class="cu-field overflow-auto" style="height: 250px">
                  <label class="cu-label">
                    <span>Work Requests Days :</span>
                  </label>
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
                      <tr v-for="(day, i) in WorkRequests" :key="i">
                        <th>{{ i + 1 }}</th>
                        <th>{{ day.Date_From }}</th>
                        <th v-if="day.Status == 1" class="text-primary">requested</th>
                        <th v-else-if="day.Status == 2" class="text-success">
                          Accepted from manger
                        </th>
                        <th v-else-if="day.Status == 3" class="text-danger">
                          Rejected from manger
                        </th>
                        <th v-else-if="day.Status == 4" class="text-success">
                          Accepted from HR
                        </th>
                        <th v-else-if="day.Status == 5" class="text-danger">
                          Rejected from HR
                        </th>
                        <th v-else-if="day.Status == 6" class="text-primary">
                          Pending day off request
                        </th>
                        <th v-else-if="day.Status == 7" class="text-success">
                          Day off request accepted from manger
                        </th>
                        <th v-else-if="day.Status == 8" class="text-success">
                          Day off request accepted from hr
                        </th>
                        <th>
                          <a href="#">
                            <i
                              class="fa fa-trash fa-lg btn text-danger"
                              aria-hidden="true"
                              @click.prevent="deleteWorkRequest(day.id, true, i)"
                            >
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
</template>

<script>
export default {
  name: "workRequest",
  data() {
    return {
      user: JSON.parse(localStorage.getItem("user")),
      multiDates: [],
      WorkRequests: [],
      NewHR_DaysOFF_Requests: {
        //Emp_id: 1,
        Date_From: "",
        Date_To: "",
        Requester_Comment: "",
        Status: 1,
      },
      apiUrl: "http://localhost:50567/app/dist/",
    };
  },
  methods: {
    SubmitWorkRequest: function () {
      let that = this,
        ObjectD = Object.assign({}, this.NewHR_DaysOFF_Requests);

      // get multi dates from calendar
      that.multiDates = Array.from(new Set(that.multiDates));

      if (this.multiDates == "") {
        swal({
          title: "Error!",
          text: "Sorry you should pick at least one day ! ",
          icon: "warning",
          dangerMode: true,
        });
      } else if (
        that.multiDates.filter((x) => new Date(x).getTime() < new Date().getTime())
          .length > 0
      ) {
        swal({
          title: "Error!",
          text: "Sorry, You can't pick a date before today's date! ",
          icon: "warning",
          dangerMode: true,
        });
      } else if (
        that.multiDates.filter(
          (x) => that.WorkRequests.filter((z) => z.Date_From == x).length > 0
        ).length > 0
      ) {
        swal({
          title: "Error!",
          text: "Sorry, You picked this day once before! ",
          icon: "warning",
          dangerMode: true,
        });
      } else {
        for (let i = 0; i < that.multiDates.length; i++) {
          //ObjectD.Emp_id = this.adminusersData[0].admin_id;
          ObjectD.Date_From = this.multiDates[i];
          ObjectD.Date_To = this.multiDates[i];

          $.ajax({
            type: "POST",
            url: that.apiUrl + "Hr_Portal/Work_Request.aspx/NewHR_DaysOFF_Requests",
            data: JSON.stringify({ HR_DayOFF_Request: ObjectD }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
              if (i == that.multiDates.length - 1) {
                swal({
                  title: "Good job!",
                  text: "You successfully added a work request ...",
                  icon: "success",
                }).then((result) => {
                  if (result) {
                    that.$router.push({ name: "home" });
                  } else {
                    swal({
                      title: "Canceled",
                      text: "Sorry, you can not add work request !",
                    });
                  }
                });
              }
            },
          });
        }
      }
    },

    deleteWorkRequest: function (id, hide, i) {
      let cobject = this;
      swal({
        title: "Are you sure you want to delete this work request?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
      }).then((result) => {
        if (result) {
          $.ajax({
            type: "POST",
            url: cobject.apiUrl + "/Hr_Portal/Work_Request.aspx/updateHide",
            data: JSON.stringify({ detail: { id: id, Canceled: hide } }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
              cobject.WorkRequests.splice(i, 1);

              swal({
                title: "Sweet!",
                text: "You successfully deleted this work request ...",
                icon: "success",
                dangerMode: true,
              });
            },
          });
        } else {
          swal({
            title: "Canceled",
            text: "Sorry, you can not delete this work request!",
          });
        }
      });
    },
  },
  created: function () {
    let cobject = this;

    //get Work_Request
    $.ajax({
      type: "POST",
      url: cobject.apiUrl + "Hr_Portal/Work_Request.aspx/getrequestsData",
      data: JSON.stringify({ user: { Emp_id: cobject.user.Emp_id } }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        cobject.requests = JSON.parse(data.d);
        cobject.WorkRequests = cobject.requests;
      },
    });

    // get multi dates from calendar
    $("body").on("click", ".ui-state-default", function (e) {
      e.preventDefault();
      cobject.multiDates.push(
        $(this).closest("td").data("year") +
          "-" +
          (parseInt($(this).closest("td").data("month")) + 1) +
          "-" +
          $(this).text()
      );
    });

    $("body").on("click", ".ui-datepicker-clear-month", function () {
      cobject.multiDates = [];
    });
  },
};
</script>

<style></style>
