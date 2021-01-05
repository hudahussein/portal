<style src='./assets/layout/css/bootstrap-material-datetimepicker.css'></style>
<style src='./assets/layout/css/bootstrap.min.css'></style>
<style src='./assets/layout/css/jQuery-UI.css'></style>
<style src='./assets/layout/css/jquery-ui.multidatespicker.css'></style>
<style src='./assets/layout/css/material-icons.css'></style>
<style src='./assets/layout/css/MaterialDesignIcons.css'></style>
<style src='./assets/layout/css/nice-select.css'></style>
<style src='./assets/layout/css/slick-theme.css'></style>
<style src='./assets/layout/css/slick.css'></style>
<style src='./assets/layout/css/main.css'></style>


<template>
  <div id="app">
    <div :class='user ? "page-container expand" : ""'>
        <side-bar v-if='user' :username='user.FullName'></side-bar>
        <div :class='user ? "main-contnet" : ""'>
          <upper-nav v-if='user' :username='user.FullName'></upper-nav>
          <router-view/>
        </div> 
    </div>
  </div>
</template>

<script>

var myDates = [];
 for (var j = 0; j <= 11; j++) {
     myDates[j] = [];
 }

function initCalendar() {
    $('div.ui-widget-header').append('\
        <a class="ui-datepicker-clear-month" title="Clear month">\
            X\
        </a>\
    ');

    var thisMonth = $($($('#calendar tbody tr')[2]).find('td')[0]).attr('data-month');
    var dateDragStart = undefined; // We'll use this variable to identify if the user is mouse button is pressed (if the user is dragging over the calendar)
    var thisDates = [];
    var calendarTds = $('.ui-datepicker-calendar td:not(.ui-datepicker-unselectable)');
    $('#calendar td').attr('data-event', '');
    $('#calendar td').attr('data-handler', '');
    $('#calendar td a').removeClass('ui-state-active');
    $('#calendar td a.ui-state-highlight').removeClass('ui-state-active').removeClass('ui-state-highlight').removeClass('ui-state-hover');
    $('#calendar td').off();
    for (var i = 0; i < myDates[thisMonth].length; i++) { // Repaint
        var a = calendarTds.find('a').filter('a:textEquals(' + myDates[thisMonth][i].getDate() + ')').addClass('ui-state-active');
        thisDates.push(new Date(a.parent().attr('data-year'), thisMonth, a.html()));
    }

    $('#calendar td').mousedown(function () {  // Click or start of dragging
        dateDragStart = new Date($(this).attr('data-year'), $(this).attr('data-month'), $(this).find('a').html());
        $(this).find('a').addClass('ui-state-active');
        return false;
    });

    $('#calendar td').mouseup(function () {
        thisDates = [];
        $('#calendar td a.ui-state-active').each(function () { //Save selected dates
            thisDates.push(new Date($(this).parent().attr('data-year'), $(this).parent().attr('data-month'), $(this).html()));
        });
        dateDragStart = undefined;
        return false;
    });
    $(document).mouseup(function () {
        dateDragStart = undefined;
    });

    $('#calendar td').mouseenter(function() {  // Drag over day on calendar
        var thisDate = new Date($(this).attr('data-year'), $(this).attr('data-month'), $(this).find('a').html());
        if (dateDragStart !== undefined && thisDate > dateDragStart) {  // We are dragging forwards
            for (var d = new Date(dateDragStart); d <= thisDate; d.setDate(d.getDate() + 1)) {
                calendarTds.find('a').filter('a:textEquals(' + d.getDate() + ')').addClass('ui-state-active');
            }
            $(this).find('a').addClass('ui-state-active');
        } else if (dateDragStart !== undefined && thisDate < dateDragStart) {  // We are dragging backwards
            for (var d = new Date(dateDragStart); d >= thisDate; d.setDate(d.getDate() - 1)) {
                calendarTds.find('a').filter('a:textEquals(' + d.getDate() + ')').addClass('ui-state-active');
            }
            $(this).find('a').addClass('ui-state-active');
        }
    });

    $('#calendar td').mouseleave(function() {
        var thisDate = new Date($(this).attr('data-year'), $(this).attr('data-month'), $(this).find('a').html());
        if (dateDragStart !== undefined && thisDate > dateDragStart) {
            for (var d = new Date(dateDragStart); d <= thisDate; d.setDate(d.getDate() + 1)) {
                if (thisDates.find( item => item.getTime() == d.getTime()) === undefined) {
                    calendarTds.find('a').filter('a:textEquals(' + d.getDate() + ')').removeClass('ui-state-active');
                }
            }
        } else if (dateDragStart !== undefined && thisDate < dateDragStart) {
            for (var d = new Date(dateDragStart); d >= thisDate; d.setDate(d.getDate() - 1)) {
                if (thisDates.find( item => item.getTime() == d.getTime()) === undefined) {
                    calendarTds.find('a').filter('a:textEquals(' + d.getDate() + ')').removeClass('ui-state-active');
                }
            }
        }
    });

    $('.ui-datepicker-clear-month').click(function () {
        thisDates = [];
        calendarTds.find('a').removeClass('ui-state-active');
    });

    $('a.ui-datepicker-next, a.ui-datepicker-prev').click(function() {
        myDates[thisMonth] = thisDates;
        initCalendar();
    });
}

import "./assets/layout/js/bootstrap-material-datetimepicker.js"
import "./assets/layout/js/jquery-nice-select.js"
import './assets/layout/js/jquery-ui.multidatespicker.js'
import "./assets/layout/js/main.js"

import upperNav from '@/components/layout/upperNav'
import sideBar from '@/components/layout/sideBar'

export default {
  name: 'App',
  components: {upperNav,sideBar},
  data() {
    return {
      user: JSON.parse(localStorage.getItem('user'))
    }
  },
  watch: {
      $route: function(to, from) {
        this.user = JSON.parse(localStorage.getItem('user'));
        $(function() {
            if ($('#calendar')) {
              $('#calendar').datepicker();
              initCalendar();
            }
        });
      }
  },
  created() {
      $(function() {
          if ($('#calendar')) {
            $('#calendar').datepicker();
            initCalendar();
          }
      });
  }
}
</script>

<style>
.main-contnet {
  margin-top:60px;
}
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
