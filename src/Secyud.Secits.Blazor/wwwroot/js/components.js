window.invokeElementMethod = function (element, method, ...params) {
    return element[method](...params);
}

window.invokeElementMethodVoid = function (element, method, ...params) {
    element[method](...params);
}


window.cancelEvent = function (eventName) {
    window[eventName] = () => false;
}

window.restoreEvent = function (eventName) {
    window[eventName] = undefined;
}