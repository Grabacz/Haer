/// <reference path="../vendors/angular.js" />

angular.module("haerApp")

.constant("contactsUrl", "http://localhost:8020/api/contacts")
.constant("salaryUrl", "http://localhost:8020/api/salary")

.config(function (contactsServiceProvider, contactsUrl) {
    contactsServiceProvider.setUrl(contactsUrl)
})

.config(function (salaryServiceProvider, salaryUrl) {
    salaryServiceProvider.setUrl(salaryUrl)
})