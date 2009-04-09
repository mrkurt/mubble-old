/**
* @fileoverview Sortable Lists.
* @author Dav Glass <dav.glass@yahoo.com>
* @version 0.1
* @class Sortable DragDrop List.
* @requires YAHOO
* @requires YAHOO.util.Dom
* @requires YAHOO.util.Event
* @requires YAHOO.util.DragDrop
*
* @constructor
* @class Sortable List.
* @param {String/HTMLElement} elm The element to convert into a sortable list
*/
YAHOO.widget.SortableList = function(elm) {
    this.elm = YAHOO.util.Dom.get(elm);
    this.drop = null;
    this.lastTarget = false;
    if (!this.elm) {
        return false;
    }
    /**
    * Custom Event
    * @type Object
    */
    this.onInit = new YAHOO.util.CustomEvent('oninit', this);
}
/**
* The init function to make the list sortable
*/
YAHOO.widget.SortableList.prototype.init = function() {
    this._setupList();
    this.onInit.fire();
}
/**
* @private
*/
YAHOO.widget.SortableList.prototype._setupList = function() {
    this.lis = this.elm.getElementsByTagName('li');
    YAHOO.util.Dom.generateId(this.lis, 'ysortable');
    for (var i = 0; i < this.lis.length; i++) {
        var ID = this.lis[i].id;
        this.lis[i]._yuiGroup = this.elm.id;
        new YAHOO.util.DDTarget(ID);
        var tmp = new YAHOO.util.DD(ID, this.elm.id);
        tmp.onDragDrop = this.onDragDrop;
        tmp.onDragOver = this.onDragOver;
        tmp.onMouseUp = this.onMouseUp;
    }
}
/**
* @private
*/
YAHOO.widget.SortableList.prototype.onDragDrop = function(ev, id) {
    var tar = YAHOO.util.Event.getTarget(ev);
    if (this.lastTarget && (id === this.lastTarget) && (tar.id != id)) {
        YAHOO.util.Dom.removeClass(this.lastTarget, 'yui-sortover');
        var tmp = $(id);
        tar.parentNode.removeChild(tar);
        tmp.parentNode.insertBefore(tar, tmp);
    }
}
/**
* @private
*/
YAHOO.widget.SortableList.prototype.onDragOver = function(ev, id) {
    if (this.lastTarget) {
        YAHOO.util.Dom.removeClass(this.lastTarget, 'yui-sortover');
    }
    this.lastTarget = id;
    YAHOO.util.Dom.addClass(id, 'yui-sortover');
}
/**
* @private
*/
YAHOO.widget.SortableList.prototype.onMouseUp = function(ev) {
    var tar = YAHOO.util.Event.getTarget(ev);
    if (this.lastTarget) {
        YAHOO.util.Dom.removeClass(this.lastTarget, 'yui-sortover');
    }
    YAHOO.util.Dom.setStyle(tar, 'position', 'static');
    YAHOO.util.Dom.setStyle(tar, 'top', '');
    YAHOO.util.Dom.setStyle(tar, 'left', '');
}
