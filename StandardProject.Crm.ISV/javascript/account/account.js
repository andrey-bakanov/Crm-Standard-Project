var Navicon = Navicon || {};

Navicon.Account = (function () {


    var nameOnChange = function (context) {
        let formContext = context.getFormContext();
        let nameAttr = formContext.getAttribute("name");

        console.log(nameAttr.getValue());

    }

    var somePrivateFunc = function (context) {
        let formContext = context.getFormContext();

        let parentsArray = formContext.getAttribute("parentaccountid").getValue();
        let parentRef = parentsArray[0];

        Xrm.Utility.showProgressIndicator("Loading.....");

        var promiseAccount = Xrm.WebApi.retrieveRecord("account", parentRef.id, "?$select=name,ftpsiteurl");
        promiseAccount.then(
            function (accountResult) {
                console.log("name:" + accountResult.name + " ftp:" + accountResult.ftpsiteurl);
                return accountResult.name;
            },
            function (error) {
                console.error(error.message);
            }
        ).then(
            function (name) {
                console.log("name:" + name);

            },
            function (error) {
                console.error(error.message);
            }
        );

        let ownerArray = formContext.getAttribute("ownerid").getValue();
        let ownerRef = ownerArray[0];
        var promiseOwner = Xrm.WebApi.retrieveRecord("systemuser", ownerRef.id, "?$select=fullname");

        Promise.all([promiseAccount, promiseOwner]).then(
            function (values) {
                console.log(values[1].fullname);
            }
        ).finally(
            function () {
                Xrm.Utility.closeProgressIndicator();
            }
        );


        Xrm.WebApi.retrieveMultipleRecors("account", "?$select=name&$filter=ownerid/systemuserid eq 'test'").then(
            function (entityResult) {
                for (var i = 0; i < entityResult.entities.length; i++) {
                    console.log(entityResult.entities[i].name);
                }
            }
        );
    }

    return {

        onLoad: function (context) {

            let formContext = context.getFormContext();
            let formType = formContext.ui.getFormType();
            if (formType == 1) {
                console.log("create type form");
            }

            formContext.getAttribute("name").addOnChange(nameOnChange);
        }
    }
})();




