﻿var sendForm = document.getElementById("send-form");
var sendButton = document.getElementById("send-button");
var messagesList = document.getElementById("messages-list");
var messageTextBox = document.getElementById("message-textbox");
var degreeSymbol = document.getElementById("degree-symbol").innerText;
var myChart;

function appendMessage(content) {
  //var li = document.createElement("li");
  //li.innerText = content;
  //messagesList.appendChild(li);

  var temp = document.getElementById("temperature");
  temp.innerText = linkToArray(content)[0].toString() + degreeSymbol;
}

function linkToArray(param) {
  return param.split(" ").map(function (item) {
    return parseFloat(item);
  });
}
function getArrayOfIdenticalValues(value, count) {
  var array = [];
  for (var i = 0; i < count; i++) {
    array.push(value);
  }
  return array;
};

function appendAlert(content) {
  var li = document.createElement("li");
  li.innerText = content;
  li.classList.add("list-group-item");
  li.classList.add("list-group-item-danger");
  messagesList.appendChild(li);
}

function appendWarning(content) {
  var li = document.createElement("li");
  li.innerText = content;
  li.classList.add("list-group-item");
  li.classList.add("list-group-item-warning");
  messagesList.appendChild(li);
}

var connection = new signalR.HubConnection("/hubs/update");

sendForm.addEventListener("submit", function () {
  var message = messageTextBox.value;
  messageTextBox.value = "";
  connection.send("Send", message);
});

connection.on("SendMessage", function (sender, message) {
  appendMessage(message);

  var ctx = document.getElementById("myChart");
  myChart = new Chart(ctx, {
    type: 'line',
    data: {
      labels: [" ", " ", " ", " ", " "],
      datasets: [{
        borderColor: "#4277f4",
        fill: false,
        label: 'Temperature',
        data: linkToArray(message).reverse(),
        borderWidth: 5,
        pointRadius: 2
      },
      {
        borderColor: "#e20000",
        fill: false,
        data: getArrayOfIdenticalValues(@Model.UpperAlarmBorder, 5),
  borderWidth: 2,
    pointRadius: 0
          },
{
  borderColor: "#e20000",
    fill: false,
      data: getArrayOfIdenticalValues(@Model.LowerAlarmBorder, 5),
  borderWidth: 2,
    pointRadius: 0
},
{
  borderColor: "#ffdd00",
    fill: false,
      data: getArrayOfIdenticalValues(@Model.UpperWarnBorder, 5),
  borderWidth: 2,
    pointRadius: 0
},
{
  borderColor: "#ffdd00",
    fill: false,
      data: getArrayOfIdenticalValues(@Model.LowerWarnBorder, 5),
  borderWidth: 2,
    pointRadius: 0
}
          ]
        },
options: {
  legend: {
    display: false
  },
  animation: false,
    elements: {
    line: {
      tension: 0
    }
  }
}
      });


    });

//connection.on("SendAction", function (sender, action) {
//  appendMessage(sender + ' ' + action);
//});

connection.on("Alert",
  function (message) {
    appendAlert(message);
  });
connection.on("Warning",
  function (message) {
    appendWarning(message);
  });

connection.start().then(function () {
  messageTextBox.disabled = false;
  sendButton.disabled = false;
});

var clickMe = document.getElementById("send-button");

clickMe.addEventListener('click', function () { }, true);

setInterval(function () {
  var event = new MouseEvent('click'),
    canceled = !clickMe.dispatchEvent(event);

  if (canceled) {
    console.log('Click event was canceled');
  }
}, @ViewBag.RefreshRate* 1000);
