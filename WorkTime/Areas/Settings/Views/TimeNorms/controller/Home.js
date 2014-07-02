

Ext.define('TimeNorms.controller.Home', {
    extend: 'Ext.app.Controller',

    requires: [
        'Common.ux.InputTextMask',
        'Common.data.Ajax',
        'Common.store.MonthList',
        'Ext.ux.grid.FiltersFeature'
    ],

    stores: [
        'TimeNorms.store.Holidays',
        'TimeNorms.store.RegNorms',
        'TimeNorms.store.RegNormGraphTypes',
        'TimeNorms.store.TimeNormsOfMonth',
        'TimeNorms.store.PostTypesDayTimeCoefficients',
        'Staffs.store.Genders'
    ],

    views: [
        'TimeNorms.view.Viewport'
    ],

    refs: [{
        ref: 'HolidaysYearCombo',
        selector: '#HolidaysYearCombo'
    }, {
        ref: 'HolidaysGrid',
        selector: '#HolidaysGrid'
    }, {
        ref: 'TimeNormsOfMonthGrid',
        selector: '#TimeNormsOfMonthGrid'
    }, {
        ref: 'TimeNormsOfMonthYearCombo',
        selector: '#TimeNormsOfMonthYearCombo'
    }],


    init: function () {

        this.control({
            '#HolidaysYearCombo': {
                select: this.HolidaysYearCombo_select
            },
            '#HolidaysShiftOnNextYear': {
                click: this.HolidaysShiftOnNextYear_click
            },
            '#TimeNormsOfMonthYearCombo': {
                select: this.TimeNormsOfMonthYearCombo_select
            },
            '#TimeNormsOfMonthShiftOnNextYear': {
                click: this.TimeNormsOfMonthShiftOnNextYear_click
            }
        });
    },

    // установка фильтра "текущий год"
    setFilter: function (year, grid, filter) {
        var store = grid.getStore();
        store.filters.clear();
        store.filter(filter, year)
        store.load();

    },

    // отправить команду переноса месячных норм времени на следующий год
    TimeNormsOfMonthShiftOnNextYear_click: function () {
        Common.data.Ajax.requestPrompt({
            url: Application.Path.Get('Settings/TimeNormsOfMonth/ShiftTimenormsOfNothOnNextYear'),
            title: 'Перенос норм времени',
            msg: 'Действительно перенести нормы времени с текущего года на следующий?',
            msgFailure: 'Нормы времени не перенесены',
            msgSuccess: 'Нормы времени перенесены на следующий год',
            params: {
                currentYear: this.getTimeNormsOfMonthYearCombo().getValue()
            }
        });
    },

    // отправить команду переноса праздников с текущего года на следующий
    ShiftHolidaysOnNextYear: function () {
        Common.data.Ajax.requestPrompt({
            url: Application.Path.Get('Settings/Holidays/ShiftHolidaysOnNextYear'),
            title: 'Перенос праздников',
            msg: 'Действительно перенести праздники с текущего года на следующий?',
            msgFailure: 'Прездики не перенесены',
            msgSuccess: 'Праздники перенесены на следующий год',
            params: {
                currentYear: this.getHolidaysYearCombo().getValue()
            }
        });
    },

    // кнопка - перенести на следующий год
    HolidaysShiftOnNextYear_click: function () {
        this.ShiftHolidaysOnNextYear();
    },

    // комбо - выбор года праздников
    HolidaysYearCombo_select: function () {
        this.setFilter(this.getHolidaysYearCombo().getValue(), this.getHolidaysGrid(), 'StartDate.Year');
    },

    // комбо - выбор года норм времени в месяц
    TimeNormsOfMonthYearCombo_select: function () {
        var year = this.getTimeNormsOfMonthYearCombo().getValue(),
            grid = this.getTimeNormsOfMonthGrid(),
            store = grid.getStore();
        store.modelDefaults.Year = year;
        this.setFilter(year, grid, 'Year');
    },

    RegNormsYearCombo_select: function () {
        var year = this.getRegNormsYearCombo().getValue(),
            grid = this.getRegNormsGrid(),
            store = grid.getStore();
        store.modelDefaults.Year = year
        this.setFilter(year, grid, 'Year');
    }

});