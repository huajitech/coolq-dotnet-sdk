using System;
using System.IO;
using HuajiTech.CoolQ.Events;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 定义用于 HuajiTech.CoolQ.Abstractions 中的类型的扩展方法。
    /// </summary>
    public static class AbstractionExtensions
    {
        /// <summary>
        /// 创建目标为指定用户的 <see cref="Messaging.Mention"/> 类的新实例。
        /// </summary>
        /// <param name="target">提及 (@) 的目标。</param>
        /// <returns>目标为指定用户的 <see cref="Messaging.Mention"/> 类的新实例。</returns>
        public static Mention Mention(this IUser target) => new Mention().SetTarget(target);

        /// <summary>
        /// 创建目标为指定用户的 <see cref="Messaging.At"/> 类的新实例。
        /// </summary>
        /// <param name="target">At (@) 的目标。</param>
        /// <returns>目标为指定用户的 <see cref="Messaging.At"/> 类的新实例。</returns>
        [Obsolete("请改为使用 Mention。")]
        public static At At(this IUser target) => (At)new At().SetTarget(target);

        /// <summary>
        /// 将 <see cref="Message"/> 实例解析为 <see cref="ComplexMessage"/> 实例。
        /// </summary>
        /// <param name="message">一个 <see cref="Message"/>实例，该 <see cref="Message"/> 实例的 <see cref="Message.Content"/> 属性为要解析的 <see cref="ComplexMessage"/> 实例的字符串表示形式。</param>
        /// <param name="useEmoji">如果要在返回的 <see cref="ComplexMessage"/> 实例中包含 <see cref="Emoji"/> 实例，则为 <see langword="true"/>；否则为 <see langword="false"/>。</param>
        /// <returns>与 <see cref="Message.Content"/> 等效的 <see cref="ComplexMessage"/> 实例。</returns>
        public static ComplexMessage Parse(this Message message, bool useEmoji = false)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return ComplexMessage.Parse(message.Content, useEmoji);
        }

        /// <summary>
        /// 获取 <see cref="Messaging.Mention"/> 实例的目标。
        /// </summary>
        /// <param name="mention">要操作的 <see cref="Messaging.Mention"/> 实例。</param>
        /// <param name="context">要使用的 <see cref="PluginContext"/>。</param>
        public static IUser GetTarget(this Mention mention, PluginContext context)
        {
            if (mention is null)
            {
                throw new ArgumentNullException(nameof(mention));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.GetUser(mention.TargetNumber);
        }

        /// <summary>
        /// 使用 <see cref="PluginContext.Current"/> 获取 <see cref="Messaging.Mention"/> 实例的目标。
        /// </summary>
        /// <param name="mention">要操作的 <see cref="Messaging.Mention"/> 实例。</param>
        public static IUser GetTarget(this Mention mention) => GetTarget(mention, PluginContext.Current);

        /// <summary>
        /// 设置 <see cref="Messaging.Mention"/> 实例的目标。
        /// </summary>
        /// <param name="mention">要操作的 <see cref="Messaging.Mention"/> 实例。</param>
        /// <param name="target"><see cref="Messaging.Mention"/> 实例的目标。</param>
        public static Mention SetTarget(this Mention mention, IUser target)
        {
            if (mention is null)
            {
                throw new ArgumentNullException(nameof(mention));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            mention.TargetNumber = target.Number;

            return mention;
        }

        /// <summary>
        /// 获取 <see cref="ContactShare"/> 实例的内容。
        /// </summary>
        /// <param name="share">要操作的 <see cref="ContactShare"/> 实例。</param>
        /// <param name="context">要使用的 <see cref="PluginContext"/>。</param>
        public static IChattable? GetContent(this ContactShare share, PluginContext context)
        {
            if (share is null)
            {
                throw new ArgumentNullException(nameof(share));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return share.ContentType switch
            {
                "qq" => context.GetUser(share.ContentNumber),
                "group" => context.GetGroup(share.ContentNumber),
                _ => null
            };
        }

        /// <summary>
        /// 使用 <see cref="PluginContext.Current"/> 获取 <see cref="ContactShare"/> 实例的内容。
        /// </summary>
        /// <param name="share">要操作的 <see cref="ContactShare"/> 实例。</param>
        public static IChattable? GetContent(this ContactShare share)
            => GetContent(share, PluginContext.Current);

        /// <summary>
        /// 设置 <see cref="ContactShare"/> 实例的内容。
        /// </summary>
        /// <param name="share">要操作的 <see cref="ContactShare"/> 实例。</param>
        /// <param name="content"><see cref="ContactShare"/> 实例的内容。</param>
        public static ContactShare SetContent(this ContactShare share, IChattable content)
        {
            if (share is null)
            {
                throw new ArgumentNullException(nameof(share));
            }

            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            share.ContentType = content switch
            {
                IUser _ => "qq",
                IGroup _ => "group",
                _ => throw new ArgumentOutOfRangeException(nameof(content)),
            };

            share.ContentNumber = content.Number;

            return share;
        }

        /// <summary>
        /// 获取指定 <see cref="Image"/> 实例表示的文件。
        /// </summary>
        /// <param name="image">要操作的 <see cref="Image"/> 实例。</param>
        /// <returns>当前 <see cref="Image"/> 实例表示的文件。</returns>
        /// <param name="context">要使用的 <see cref="PluginContext"/>。</param>
        public static FileInfo GetFile(this Image image, PluginContext context)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Bot.GetImage(image.FileName!);
        }

        /// <summary>
        /// 使用 <see cref="PluginContext.Current"/> 获取指定 <see cref="Image"/> 实例表示的文件。
        /// </summary>
        /// <param name="image">要操作的 <see cref="Image"/> 实例。</param>
        public static FileInfo GetFile(this Image image) => GetFile(image, PluginContext.Current);

        /// <summary>
        /// 获取指定 <see cref="Record"/> 实例表示的文件。
        /// </summary>
        /// <param name="record">要操作的 <see cref="Record"/> 实例。</param>
        /// <param name="format">返回的文件的格式。</param>
        /// <returns>当前 <see cref="Record"/> 实例表示的文件。</returns>
        /// <param name="context">要使用的 <see cref="PluginContext"/>。</param>
        public static FileInfo GetFile(this Record record, string format, PluginContext context)
        {
            if (record is null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Bot.GetRecord(record.FileName!, format);
        }

        /// <summary>
        /// 使用 <see cref="PluginContext.Current"/> 获取指定 <see cref="Record"/> 实例表示的文件。
        /// </summary>
        /// <param name="record">要操作的 <see cref="Record"/> 实例。</param>
        /// <param name="format">返回的文件的格式。</param>
        public static FileInfo GetFile(this Record record, string format)
            => GetFile(record, format, PluginContext.Current);

        /// <summary>
        /// 向指定聊天发送一个 <see cref="ISendable"/> 实例。
        /// </summary>
        /// <param name="sendee">目标可被发送实例。</param>
        /// <param name="message">要发送的 <see cref="ComplexMessage"/> 实例。</param>
        /// <returns>一个 <see cref="Message"/> 实例，表示已发送的消息。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sendee"/> 为 <see langword="null"/>。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <see langword="null"/>。</exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> 不包含任何元素，或其等效字符串表示形式为 <see cref="string.Empty"/>。</exception>
        /// <exception cref="ApiException">酷Q返回了指示操作失败的值。</exception>
        public static Message Send(this ISendee sendee, ISendable message)
        {
            if (sendee is null)
            {
                throw new ArgumentNullException(nameof(sendee));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return sendee.Send(message.ToSendableString());
        }

        /// <summary>
        /// 回复指定的消息接收事件。
        /// </summary>
        /// <param name="e">事件数据。</param>
        /// <param name="message">要回复的字符串。</param>
        /// <returns>发送的消息。</returns>
        public static Message Reply(this MessageReceivedEventArgs e, string message)
            => e.Source is IGroup ? e.Source.Send(e.Sender.Mention() + " " + message) :
               e.Source.Send(message);

        /// <summary>
        /// 回复指定的消息接收事件。
        /// </summary>
        /// <param name="e">事件数据。</param>
        /// <param name="message">要回复的 <see cref="ISendable"/> 实例。</param>
        /// <returns>发送的消息。</returns>
        public static Message Reply(this MessageReceivedEventArgs e, ISendable message)
            => e.Reply(message.ToSendableString());
    }
}