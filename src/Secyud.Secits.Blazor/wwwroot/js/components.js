window.invokeElementMethod = function (element, method, ...params) {
    return element[method](...params);
}

window.invokeElementMethodVoid = function (element, method, ...params) {
    element[method](...params);
}