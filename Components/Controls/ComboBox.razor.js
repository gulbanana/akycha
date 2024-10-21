export function bind(parent, child) {
    child.style.minWidth = (parent.scrollWidth + 2) + "px";
    Popper.createPopper(parent, child, {
        placement: "bottom-start"
    });
}