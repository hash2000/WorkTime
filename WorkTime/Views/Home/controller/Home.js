

Ext.define('WorkTime.controller.Home', {
    extend: 'Ext.app.Controller',

    refs: [{
        ref: 'VacationButton',
        selector: '#VacationButton'
    }, {
        ref: 'TableButton',
        selector: '#TableButton'
    }],

    init: function () {
        this.control({
            '#VacationButton': {
                click: this.VacationButton_click
            },
            '#TableButton': {
                click: this.TableButton_click
            }
        });        
    },

    VacationButton_click: function () {


    },

    TableButton_click: function () {


    }

});