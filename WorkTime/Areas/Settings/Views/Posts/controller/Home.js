


Ext.define('Posts.controller.Home', {
    extend: 'Ext.app.Controller',

    requires: [
        'Ext.ux.grid.FiltersFeature'
    ],

    stores: [
        'Posts.store.Posts',
        'Posts.store.PostTypes',
        'Posts.store.VacationTypesInPostType',
        'Vacations.store.VacationTypes',
        'TimeNorms.store.PostTypesDayTimeCoefficients'
    ],

    
    refs: [{
        ref: 'PostTypesGrid',
        selector: '#PostTypesGrid'
    }, {
        ref: 'PostsVacationTypesWindow',
        selector: 'PostsVacationTypesWindow'
    }, {
        ref: 'VacationTypesGrid',
        selector: '#VacationTypesGrid'
    }],
    
    init: function () {
        this.control({
            '#PostTypesDayTimeCoefficientButton': {
                click: this.PostTypesDayTimeCoefficientButton_click
            },
            'PostsVacationTypesWindow button[action=save]': {
                click: this.PostsVacationTypesWindowSave_clisk
            },
            '#PostTypesGrid': {
                select: this.PostTypesGrid_select
            }
        });
    },

    PostTypesGrid_select: function (scope, record) {
        // при выборе нового типа должности 
        //  нужно перезагрузить хранилище
        var grid = this.getVacationTypesGrid(),
            store = grid.getStore();
        store.proxy.extraParams['postTypeId'] = record.get('Id');
        store.load();
    },

    PostTypesDayTimeCoefficientButton_click: function () {
        // вызов диалогового окна с выбором набора типов отпусков 
        //  для текущего типа должности
        var grid = this.getPostTypesGrid(),
            store = grid.getStore(),
            selection = grid.getSelectionModel().getSelection()[0];
        if (Ext.isEmpty(selection))
            return;
        var dlg = Ext.create('Posts.view.window.VacationTypes');
        dlg.down('form').loadRecord(selection);
        dlg.show();
    },

    PostsVacationTypesWindowSave_clisk: function () {
        // в диалоговом окне выбора набора типов отпусков 
        //  нажата кнопка сохранить
        var form = this.getPostsVacationTypesWindow().down('form');
        if (form.isValid()) {
            var values = form.getValues(),
                record = form.getRecord();

        }
    }

});