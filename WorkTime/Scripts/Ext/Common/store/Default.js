
Ext.define('Common.store.Default', {
    extend: 'Ext.data.Store',
    pageSize: 50,
    autoLoad: true,

    constructor: function (config) {
        this.callParent(arguments);
        this.proxy.on('exception', this.onProxyException, this);
    },

    /**
   @private
   При возникновении ошибки, отобразить её текст во всплывающем сообщении.
   Обработать ошибки при создании и удалении.
   */
    onProxyException: function (proxy, response, operation) {
        Common.Popup.red('Ошибка', operation.getError() || '');

        if (operation.action == 'create') {
            //for (var i = 0; i < operation.records.length; i++) {
            //    var rec = operation.records[i];
            //    var store = rec.store;
            //    store.remove(rec);
            //    store.sync();
            //}
        }
        else if (operation.action == 'destroy') {
            //Ext.each(this.removed, function (record) {
            //    this.insert(record.index, record);
            //}, this);
        }

        this.removed = [];
    }

});