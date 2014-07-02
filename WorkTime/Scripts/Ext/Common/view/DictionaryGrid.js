
///
/// объект, предоставляющий методы обносления записей
///

var updareRecord = {
    updateStore: function (grid) {
        var el = grid,
            store = el.getStore();
        store.load();
    },

    createAndEditRecord: function (grid, followPluginId) {
        if (Ext.isEmpty(grid) || Ext.isEmpty(followPluginId))
            return;
        var el = grid,
            store = el.getStore(),
            editPlugin = el.getPlugin(followPluginId);

        var model = store.createModel(store.model);
        store.insert(0, model);
        editPlugin.startEdit(0, 0);
    },

    removeCurrentRecord: function (grid, dataNameField) {
        var el = grid,
            store = el.getStore(),
            selection = el.getSelectionModel().getSelection()[0];
        if (Ext.isEmpty(selection))
            return;

        var confirmText = 'Вы действительно хотите удалить элемент';
        if (!Ext.isEmpty(this.dataNameField)) {
            var nametext = selection.data[this.dataNameField];
            confirmText += ':<br/>"' + nametext + '"';
        }
        confirmText += ' ?';
        Ext.MessageBox.confirm('Внимание!', confirmText, function (btn) {
            if (btn == 'yes') {
                store.remove(selection);
            }
        });
    },
};


Ext.define('Common.view.DictionaryGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.CommonDictionaryGrid',

    columnLines: true,
    enableLocking: true,
    split: true,
    scroll: 'both'

    
    /*
        эта кнопка не нужна, потому что обновление будет в 
        элементе pagingtoolbar
    , {
        xtype: 'button',
        text: 'Обновить',
        itemId: 'CommonDictionaryGridUpdateButton',
        handler: function () {
            var parent = this.up('CommonDictionaryGrid');
            updareRecord.updateStore(parent);
        }
    }*/,

    initComponent: function () {

        var tbar = [{
            xtype: 'button',
            text: 'Добавить',
            itemId: 'CommonDictionaryGridAddButton',
            handler: function () {
                var parent = this.up('CommonDictionaryGrid');
                updareRecord.createAndEditRecord(parent, 'RowEditorPlugin');
            }
        }, {
            xtype: 'button',
            text: 'Удалить',
            itemId: 'CommonDictionaryGridDeleteButton',
            handler: function () {
                var parent = this.up('CommonDictionaryGrid');
                updareRecord.removeCurrentRecord(parent, 'RowEditorPlugin');
            }
        }];

        if (Ext.isEmpty(this.tbar)) {
            this.tbar = tbar;
        } else {
            for (var i = 0 ; i < this.tbar.length; i++) {
                tbar.push(this.tbar[i]);
            }
            this.tbar = tbar;
        }

        this.callParent(arguments);

        // добавление плагина для строчного редактирования 
        if (Ext.isEmpty(this.plugins))
            this.plugins = [];
        this.plugins.push(
            Ext.create('Ext.grid.plugin.RowEditing', {
                    pluginId: 'RowEditorPlugin'
                })
            );
    }


});