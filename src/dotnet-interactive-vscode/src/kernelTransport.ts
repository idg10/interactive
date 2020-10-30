import {
    MessageTransport,
    KernelTransport,
    KernelEventEnvelopeObserver,
    KernelCommand,
    KernelCommandType,
    KernelCommandEnvelope,
    DisposableSubscription,
    LabelledMessageObserver,
    MessageObserver
} from "./contracts";

export function kernelTransportFromMessageTransport(messageTransport: MessageTransport): KernelTransport {

    return {
        subscribeToKernelEvents: (observer: KernelEventEnvelopeObserver): DisposableSubscription => {
            return messageTransport.subscribeToMessagesWithLabel(
                "kernelEvents",
                observer);
        },

        submitCommand: (command: KernelCommand, commandType: KernelCommandType, token: string): Promise<void> => {
            let envelope: KernelCommandEnvelope = {
                commandType: commandType,
                command: command,
                token: token,
            };
            return messageTransport.sendMessage("commands", envelope);
        },

        subscribeToMessagesWithLabelPrefix<T extends object>(label: string, observer: LabelledMessageObserver<T>): DisposableSubscription {
            return messageTransport.subscribeToMessagesWithLabelPrefix(`user/${label}`, observer);
        },

        subscribeToMessagesWithLabel<T extends object>(label: string, observer: MessageObserver<T>): DisposableSubscription {
            return messageTransport.subscribeToMessagesWithLabel(`user/${label}`, observer);
        },

        sendMessage<T>(label: string, message: T): Promise<void> {
            console.log("Kernel transport adapter sending message on " + label);
            return messageTransport.sendMessage(`user/${label}`, message);
        },

        waitForReady: (): Promise<void> => {
            return messageTransport.waitForReady();
        },

        dispose: (): void => {
        }
    };
}