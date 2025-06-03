export const getDocumentEventHandler = function () {
    let record = 0;
    const allEventListener = {};

    const generateFunc = function (invoker) {
        const m = function (e) {
            return {
                Detail: e.detail,
                ScreenX: e.screenX,
                ScreenY: e.screenY,
                ClientX: e.clientX,
                ClientY: e.clientY,
                OffsetX: e.offsetX,
                OffsetY: e.offsetY,
                PageX: e.pageX,
                PageY: e.pageY,
                MovementX: e.movementX,
                MovementY: e.movementY,
                Button: e.button,
                Buttons: e.buttons,
                CtrlKey: e.ctrlKey,
                ShiftKey: e.shiftKey,
                AltKey: e.altKey,
                MetaKey: e.metaKey,
                Type: e.type,
            }
        }


        return function (e) {
            invoker.invokeMethodAsync('Invoke', m(e));
        }
    }


    return {
        addEventListener: function (type, invoker) {
            let id = record++;
            let func = generateFunc(invoker);
            document.addEventListener(type, func);
            allEventListener[id] = {
                func, type
            };
            return id;
        },
        removeEventListener: function (id) {
            let listener = allEventListener[id];
            document.removeEventListener(listener.type, listener.func);
            delete allEventListener[id];
        }
    }
}