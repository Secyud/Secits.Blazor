const elementDict = {};
window.onInputTextChange = (element, value) => {
    elementDict[element] = value;
    setTimeout(() => {
        const text = elementDict[element];
        if (text === value) {
            element.innerHTML = value;
            elementDict[element] = undefined;
        }
    }, 500);
}