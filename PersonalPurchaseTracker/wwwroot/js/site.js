
//viewModel bound to the Purchase Tracker
var viewModel = kendo.observable({
    isVisible: true,
    selectedCategory: null,
    totalTransactionCost: "",
    averageTransactionCost: "",
    newPurchaseDate: new Date(),
    newPayee: "",
    newPurchaseAmount: "",
    newMemo: "",
    newPurchaseCategory: undefined,
    newCategoryID: 0,

    //Datasource defiinition for CRUD operations related to Transactions
    transactions: new kendo.data.DataSource({
        totalTransactionCost: "TEST",
        schema: {
            model: {
                id: "id",
                fields: {
                    id: { type: "number", defaultValue: 0 }
                }
            }
        },
        batch: true,
        pageSize: 3,
        transport: {
            autoSync: true,
            read: {
                url: "/api/Transactions",
                dataType: "json"
            },
            create: {
                url: function(item) {
                    console.log("made it here");
                    return "/api/Transactions/Kendo";
                },
                type: "post",
                dataType: "json"
            },
            update: {
                url: function(item) {
                    return "/api/Transactions/Kendo/" + item.models[0].id;
                },
                type: "put",
                dataType: "json"
            },
            destroy: {
                url: function(item) {
                    return "/api/Transactions/" + item.models[0].id;
                },
                type: "delete",
                dataType: "json"
            },
            parameterMap: function(options, operation) {
                if (operation !== "read" && options.models) {
                    return { models: kendo.stringify(options.models[0]) };
                }
            }
        },
        sort: { field: "purchaseDate", dir: "desc" },
        change: function(e) {
            //If there has been an update to the list data
            //Only update the Cost and Average on save
            if (!e.action || e.action === "sync") {
                var data = this.data();
                var total = 0;
                data.forEach(function(element) {
                    total += element.purchaseAmount;
                });
                viewModel.set("totalTransactionCost", total.toFixed(2));
                viewModel.set("averageTransactionCost", (total / data.length).toFixed(2));
            }
        }
    }),
    //Definition for Submit new ListItem
    onSubmit: function(e) {
        e.preventDefault();
        //This extracts data from the form submission.  Essentially, I get the form data
        //and add it to the datasource.  It is handled this way because it allows the asp/razor
        //defined validations to automatically kick off
        var $form = $('#aspForm');
        if ($form.valid()) {
            this.newCategoryName = $('#newCategory option:selected').text();
            this.transactions.insert({
                id: 0,
                payee: this.newPayee,
                memo: this.newMemo,
                categoryID: this.newCategoryID,
                purchaseAmount: this.newPurchaseAmount,
                purchaseDate: this.newPurchaseDate,
                purchaseCategory: {
                    id: this.newCategoryID,
                    categoryName: this.newCategoryName
                }
            });
            this.transactions.sync();
        }
    },
    //DataSource for category data
    categories: new kendo.data.DataSource({
        transport: {
            read: {
                url: "/api/Categories",
                dataType: "json"
            }
        }
    }),
});

//Bind the viewModel to the PurchaseTracking page for MVVM access
kendo.bind($("#purchaseTracking"), viewModel);
kendo.bind($("#totalCost"), viewModel);
kendo.bind($("#averageCost"), viewModel);

