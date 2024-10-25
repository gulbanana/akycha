export function bind(parent, child) {
    Popper.createPopper(parent, child, {
        placement: "bottom-start",
        modifiers: [{
            name: "copyReferenceWidth",
            enabled: true,
            phase: "beforeWrite",
            requires: ["computeStyles"],
            fn: ({ state }) => {
                state.styles.popper.width = `${state.rects.reference.width}px`;
            },
        }]
    });
}